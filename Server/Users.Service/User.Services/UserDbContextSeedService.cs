using Grpc.Core;
using GrpcUsersSeeder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using User.DataAccess;
using User.Models;
using User.Services.Contracts;

namespace User.Services
{
    public class UserDbContextSeedService : UsersSeeder.UsersSeederBase
    {
        private const int AdminsCount = 1;
        private const int UsersCount = 50;
        private const string AdminRoleName = "Admin";

        private readonly UserDbContext context;
        private readonly IAuthService authService;
        private readonly IQueueSenderService messageQueue;

        public UserDbContextSeedService(
            UserDbContext context,
            IAuthService authService,
            IQueueSenderService messageQueue)
        {
            this.context = context;
            this.authService = authService;
            this.messageQueue = messageQueue;
        }

        public override async Task<SeedUsersReply> SeedUsersDatabase(SeedUsersRequest request, ServerCallContext context)
        {
            await this.SeedRolesAsync(AdminRoleName);
            await this.SeedUsersAsync(UsersCount);
            await this.SeedUsersAsync(AdminsCount, AdminRoleName);

            return new SeedUsersReply { Seeded = true };
        }

        private async Task SeedRolesAsync(string roleName)
        {
            if (await this.context.Roles.AnyAsync())
            {
                return;
            }

            var role = new Role { Name = roleName };

            await this.context.Roles.AddAsync(role);
            await this.context.SaveChangesAsync();
        }

        private async Task SeedUsersAsync(int usersCount)
        {
            if (await this.context.Users.AnyAsync(u => !u.Roles.Any()))
            {
                return;
            }

            var users = new List<Models.User>();
            for (int i = 1; i <= usersCount; i++)
            {
                byte[] salt = this.authService.GenerateSalt();

                var user = new Models.User
                {
                    Username = $"User{i}",
                    Password = this.authService.HashPassword("123", salt),
                    Salt = salt
                };

                users.Add(user);
            }

            await this.context.Users.AddRangeAsync(users);
            await this.context.SaveChangesAsync();

            var userMessages = users
                .Select(u => new { u.Username, UserId = u.Id })
                .ToList();

            foreach (var user in userMessages)
            {
                var userAsString = JsonSerializer.Serialize(user);
                this.messageQueue.PostQueueMessage("users", userAsString);
            }
        }

        private async Task SeedUsersAsync(int usersCount, string role)
        {
            if (await this.context.Users.AnyAsync(u => u.Roles.Any(r => r.Role.Name == role)))
            {
                return;
            }

            Guid roleId = await this.context
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
                byte[] salt = this.authService.GenerateSalt();

                var user = new Models.User
                {
                    Username = $"{role}{i}",
                    Password = this.authService.HashPassword("123", salt),
                    Salt = salt
                };

                var userRole = new UserRole
                {
                    RoleId = roleId
                };

                user.Roles.Add(userRole);
                users.Add(user);
            }

            await this.context.Users.AddRangeAsync(users);
            await this.context.SaveChangesAsync();

            var userMessages = users
                .Select(u => new { u.Username, UserId = u.Id })
                .ToList();

            foreach (var user in userMessages)
            {
                var userAsString = JsonSerializer.Serialize(user);
                this.messageQueue.PostQueueMessage("users", userAsString);
            }
        }
    }
}
