using GameBox.Core;
using GameBox.Core.Enums;
using GameBox.Data;
using GameBox.Data.Models;
using GameBox.Services.Contracts;
using GameBox.Services.Models.View.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameBox.Services
{
    public class UserService : Service, IUserService
    {
        private const int UsersInPage = 15;
        private const string UserLockedState = "User {0} is currently {1}";

        public UserService(GameBoxDbContext database)
            : base(database)
        {
        }

        public int GetTotalEntries()
        {
            return Database.Users.Count();
        }

        public ServiceResult AddRole(string username, string roleName)
        {
            var userRoleInfo = Database
                .UserRoles
                .Where(ur => ur.Role.Name == roleName)
                .Where(ur => ur.User.Username == username)
                .Select(ur => new
                {
                    ur.RoleId,
                    ur.UserId
                })
                .FirstOrDefault();

            if (userRoleInfo != null)
            {
                return GetServiceResult(
                    $"User {username} is already in {roleName} role.",
                    ServiceResultType.Fail);
            }

            Guid? userId = Database
                .Users
                .Where(u => u.Username == username)
                .Select(u => new { u.Id })
                .FirstOrDefault()?
                .Id;

            Guid? roleId = Database
                .Roles
                .Where(r => r.Name == roleName)
                .Select(r => new { r.Id })
                .FirstOrDefault()?
                .Id;

            if (userId == null || roleId == null)
            {
                return GetServiceResult(
                    $"User {username} or {roleName} role is not present in database.",
                    ServiceResultType.Fail);
            }

            UserRoles userRole = new UserRoles
            {
                RoleId = roleId.Value,
                UserId = userId.Value
            };

            Database.UserRoles.Add(userRole);
            Database.SaveChanges();

            return GetServiceResult(
                $"User {username} successfully added to {roleName} role.",
                ServiceResultType.Success);
        }

        public ServiceResult RemoveRole(string username, string roleName)
        {
            UserRoles userRole = Database
                .UserRoles
                .Where(ur => ur.Role.Name == roleName)
                .Where(ur => ur.User.Username == username)
                .FirstOrDefault();

            if (userRole == null)
            {
                return GetServiceResult(
                    $"User {username} is not in {roleName} role.",
                    ServiceResultType.Fail);
            }

            Database.UserRoles.Remove(userRole);
            Database.SaveChanges();

            return GetServiceResult(
                $"User {username} successfully removed from {roleName} role.",
                ServiceResultType.Success);
        }

        public ServiceResult Lock(string username)
        {
            User user = Database
                .Users
                .Where(u => u.Username == username)
                .FirstOrDefault();

            if (user == null)
            {
                return GetServiceResult(
                    string.Format(Constants.Common.NotExistingEntry, nameof(User), username),
                    ServiceResultType.Fail);
            }

            if (user.IsLocked)
            {
                return GetServiceResult(
                    string.Format(UserLockedState, username, Constants.Common.Locked),
                    ServiceResultType.Fail);
            }

            user.IsLocked = true;

            Database.SaveChanges();

            return GetServiceResult(
                string.Format(Constants.Common.Success, nameof(User), username, Constants.Common.Locked),
                ServiceResultType.Success);
        }

        public ServiceResult Unlock(string username)
        {
            User user = Database
                .Users
                .Where(u => u.Username == username)
                .FirstOrDefault();

            if (user == null)
            {
                return GetServiceResult(
                    string.Format(Constants.Common.NotExistingEntry, nameof(User), username),
                    ServiceResultType.Fail);
            }

            if (!user.IsLocked)
            {
                return GetServiceResult(
                    string.Format(UserLockedState, username, Constants.Common.Unlocked),
                    ServiceResultType.Fail);
            }

            user.IsLocked = false;

            Database.SaveChanges();

            return GetServiceResult(
                string.Format(Constants.Common.Success, nameof(User), username, Constants.Common.Unlocked),
                ServiceResultType.Success);
        }

        public IEnumerable<ListUsersViewModel> All(string userQuery)
        {
            IQueryable<User> query = Database.Users;

            if (!string.IsNullOrEmpty(userQuery)
                && !string.IsNullOrWhiteSpace(userQuery))
            {
                query = query
                    .Where(u => u.Username.ToLower().Contains(userQuery.ToLower()));
            }

            return query
                .OrderBy(u => u.Username)
                .Take(UsersInPage)
                .Select(u => new ListUsersViewModel
                {
                    Id = u.Id,
                    Username = u.Username,
                    IsLocked = u.IsLocked,
                    IsAdmin = u.Roles.Any(r => r.Role.Name == Constants.Common.Admin)
                })
                .ToList();
        }
    }
}