using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using User.DataAccess;
using User.Models;
using User.Services.Contracts;
using User.Services.Exceptions;
using User.Services.Infrastructure;
using User.Services.ViewModels.Users;

namespace User.Services;

public class UserService : IUserService
{
    private const int UsersOnPage = 15;

    private readonly IMapper mapper;
    private readonly UserDbContext database;

    public UserService(
        IMapper mapper,
        UserDbContext database)
    {
        this.mapper = mapper;
        this.database = database;
    }

    public async Task<IEnumerable<UsersListViewModel>> GetAllUsersAsync(string username)
    {
        IQueryable<Models.User> query = this.database.Users;

        if (!string.IsNullOrWhiteSpace(username))
        {
            query = query.Where(u => u.Username.ToLower().Contains(username.ToLower()));
        }

        return await query
            .OrderBy(u => u.Username)
            .Take(UsersOnPage)
            .ProjectTo<UsersListViewModel>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<string> LockAsync(string username)
    {
        var user = await this.database
            .Users
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new NotFoundException(nameof(User), username);
        }

        if (user.IsLocked)
        {
            throw new AccountLockedException(username);
        }

        user.IsLocked = true;

        await this.database.SaveChangesAsync();

        return string.Format(Constants.Common.Success, nameof(User), username, Constants.Common.Locked);
    }

    public async Task<string> UnockAsync(string username)
    {
        var user = await this.database
            .Users
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new NotFoundException(nameof(User), username);
        }

        if (!user.IsLocked)
        {
            throw new AccountUnlockedException(username);
        }

        user.IsLocked = false;

        await this.database.SaveChangesAsync();

        return string.Format(Constants.Common.Success, nameof(User), username, Constants.Common.Unlocked);
    }

    public async Task<string> AddRoleAsync(string username, string roleName)
    {
        var userRoleInfo = await this.database
            .UserRoles
            .Where(ur => ur.Role.Name == roleName)
            .Where(ur => ur.User.Username == username)
            .Select(ur => new
            {
                ur.RoleId,
                ur.UserId
            })
            .FirstOrDefaultAsync();

        if (userRoleInfo != null)
        {
            throw new RoleEditException(username, roleName, true);
        }

        var userId = await this.database
            .Users
            .Where(u => u.Username == username)
            .Select(u => u.Id)
            .FirstOrDefaultAsync();

        if (userId == default(Guid))
        {
            throw new NotFoundException(nameof(User), username);
        }

        var roleId = this.database
            .Roles
            .Where(r => r.Name == roleName)
            .Select(r => r.Id)
            .FirstOrDefault();

        if (roleId == default(Guid))
        {
            throw new NotFoundException(nameof(Role), roleName);
        }

        var userRole = new UserRole
        {
            RoleId = roleId,
            UserId = userId
        };

        await this.database.UserRoles.AddAsync(userRole);
        await this.database.SaveChangesAsync();

        return $"User {username} successfully added to {roleName} role.";
    }

    public async Task<string> RemoveRoleAsync(string username, string roleName)
    {
        var userRole = await this.database
            .UserRoles
            .Where(ur => ur.Role.Name == roleName)
            .Where(ur => ur.User.Username == username)
            .FirstOrDefaultAsync();

        if (userRole == null)
        {
            throw new RoleEditException(username, roleName, false);
        }

        this.database.UserRoles.Remove(userRole);
        await this.database.SaveChangesAsync();

        return $"User {roleName} successfully removed from {username} role.";
    }
}
