using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using User.Models;

namespace User.DataAccess
{
    public static class UserDbContextSeed
    {
        private const int AdminsCount = 1;
        private const int UsersCount = 50;
        private const string AdminRoleName = "Admin";

        private static UserDbContext context;
        private static Action<string, string> postQueueMessage;
        private static Func<byte[]> generateSalt;
        private static Func<string, byte[], string> hashPassword;

        public static async Task SeedDatabaseAsync(
            UserDbContext database,
            Action<string, string> postQueueMessageFunc,
            Func<byte[]> generateSaltFunc,
            Func<string, byte[], string> hashPasswordFunc)
        {
            context = database;
            postQueueMessage = postQueueMessageFunc;
            generateSalt = generateSaltFunc;
            hashPassword = hashPasswordFunc;

            await SeedRolesAsync(AdminRoleName);
            await SeedUsersAsync(UsersCount);
            await SeedUsersAsync(AdminsCount, AdminRoleName);
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

            var users = new List<Models.User>();
            for (int i = 1; i <= usersCount; i++)
            {
                byte[] salt = generateSalt();

                var user = new Models.User
                {
                    Username = $"User{i}",
                    Password = hashPassword("123", salt),
                    Salt = salt
                };

                users.Add(user);
            }

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();

            var userMessages = users
                .Select(u => new { u.Username, UserId = u.Id })
                .ToList();

            foreach (var user in userMessages)
            {
                var userAsString = JsonSerializer.Serialize(user);
                postQueueMessage("users", userAsString);
            }
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

            var users = new List<Models.User>();
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
                users.Add(user);
            }

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();

            var userMessages = users
                .Select(u => new { u.Username, UserId = u.Id })
                .ToList();

            foreach (var user in userMessages)
            {
                var userAsString = JsonSerializer.Serialize(user);
                postQueueMessage("users", userAsString);
            }
        }
    }
}
