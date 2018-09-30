using GameBox.Core;
using GameBox.Data;
using GameBox.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace GameBox.Api.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private const int AdminsCount = 1;
        private const int UsersCount = 50;

        private static readonly Random random = new Random();

        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<GameBoxDbContext>().Database.Migrate();
            }

            return app;
        }

        public static IApplicationBuilder UseSeedDatabase(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                GameBoxDbContext db = serviceScope.ServiceProvider.GetService<GameBoxDbContext>();

                Seed(db);
            }

            return app;
        }

        private static void Seed(GameBoxDbContext db)
        {
            SeedRoles(db, Constants.Common.Admin);
            SeedUsers(db, AdminsCount, Constants.Common.Admin);
            SeedUsers(db, UsersCount);
        }

        private static void SeedRoles(GameBoxDbContext db, string roleName)
        {
            if (db.Roles.Any())
            {
                return;
            }

            Role role = new Role { Name = roleName };

            db.Roles.Add(role);
            db.SaveChanges();
        }

        private static void SeedUsers(GameBoxDbContext db, int usersCount)
        {
            if (db.Users.Any(u => !u.Roles.Any()))
            {
                return;
            }

            for (int i = 1; i <= usersCount; i++)
            {
                byte[] salt = GenerateSalt();

                User user = new User
                {
                    Username = $"User{i}",
                    Password = HashPassword("123", salt),
                    Salt = salt
                };

                db.Users.Add(user);
            }

            db.SaveChanges();
        }

        private static void SeedUsers(GameBoxDbContext db, int usersCount, string role)
        {
            if (db.Users.Any(u => u.Roles.Any(r => r.Role.Name == role)))
            {
                return;
            }

            Guid roleId = db
                .Roles
                .Where(r => r.Name == role)
                .Select(r => r.Id)
                .FirstOrDefault();

            if (roleId == default(Guid))
            {
                return;
            }

            for (int i = 1; i <= usersCount; i++)
            {
                byte[] salt = GenerateSalt();

                User user = new User
                {
                    Username = $"{role}{i}",
                    Password = HashPassword("123", salt),
                    Salt = salt
                };

                user.Roles.Add(new UserRole
                {
                    RoleId = roleId
                });

                db.Users.Add(user);
            }

            db.SaveChanges();
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private static string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
        }
    }
}