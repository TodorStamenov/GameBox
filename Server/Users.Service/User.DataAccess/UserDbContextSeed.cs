using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Models;

namespace User.DataAccess
{
    public static class UserDbContextSeed
    {
        private const int AdminsCount = 1;
        private const int UsersCount = 50;
        private const string AdminRoleName = "Admin";

        private static UserDbContext context;
        private static Func<byte[]> generateSalt;
        private static Func<string, byte[], string> hashPassword;

        public static async Task SeedDatabaseAsync(
            UserDbContext database,
            Action<string, string> postQueueMessage,
            Func<byte[]> generateSaltFunc,
            Func<string, byte[], string> hashPasswordFunc)
        {
            context = database;
            generateSalt = generateSaltFunc;
            hashPassword = hashPasswordFunc;

            await SeedRolesAsync(AdminRoleName);
            await SeedUsersAsync(UsersCount);
            await SeedUsersAsync(AdminsCount, AdminRoleName);

            var users = await context
                .Users
                .Select(u => new { u.Username, UserId = u.Id })
                .ToListAsync();

            foreach (var user in users)
            {
                var userAsString = JsonSerializer.Serialize(user);
                postQueueMessage("users", userAsString);
            }
        }

        private static async Task SeedRolesAsync(string roleName)
        {
            if (await context.Roles.AnyAsync())
            {
                return;
            }

            var role = new Role { Name = roleName };

            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();
        }

        private static async Task SeedUsersAsync(int usersCount)
        {
            if (await context.Users.AnyAsync(u => !u.Roles.Any()))
            {
                return;
            }

            for (int i = 1; i <= usersCount; i++)
            {
                byte[] salt = generateSalt();

                var user = new Models.User
                {
                    Username = $"User{i}",
                    Password = hashPassword("123", salt),
                    Salt = salt
                };

                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedUsersAsync(int usersCount, string role)
        {
            if (await context.Users.AnyAsync(u => u.Roles.Any(r => r.Role.Name == role)))
            {
                return;
            }

            Guid roleId = await context
                .Roles
                .Where(r => r.Name == role)
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            if (roleId == default)
            {
                return;
            }

            for (int i = 1; i <= usersCount; i++)
            {
                byte[] salt = generateSalt();

                var user = new Models.User
                {
                    Username = $"{role}{i}",
                    Password = hashPassword("123", salt),
                    Salt = salt
                };

                var userRole = new UserRole
                {
                    RoleId = roleId
                };

                user.Roles.Add(userRole);

                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
