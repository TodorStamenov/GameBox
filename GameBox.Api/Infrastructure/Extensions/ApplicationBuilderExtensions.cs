using GameBox.Core;
using GameBox.Data;
using GameBox.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace GameBox.Api.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private const int AdminsCount = 1;
        private const int UsersCount = 50;
        private const int CategoriesCount = 7;
        private const int GamesCount = CategoriesCount * 10;

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
            SeedCategories(db, CategoriesCount);
            SeedGames(db, GamesCount);
            SeedOrders(db);
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

                user.Roles.Add(new UserRoles
                {
                    RoleId = roleId
                });

                db.Users.Add(user);
            }

            db.SaveChanges();
        }

        private static void SeedCategories(GameBoxDbContext db, int categoriesCount)
        {
            if (db.Categories.Any())
            {
                return;
            }

            for (int i = 1; i <= categoriesCount; i++)
            {
                db.Categories.Add(new Category
                {
                    Name = $"Category{i}"
                });
            }

            db.SaveChanges();
        }

        private static void SeedGames(GameBoxDbContext db, int gamesCount)
        {
            if (db.Games.Any())
            {
                return;
            }

            List<Guid> categoryIds = db
                .Categories
                .Select(c => c.Id)
                .ToList();

            for (int i = 1; i <= gamesCount; i++)
            {
                db.Games.Add(new Game
                {
                    Title = $"Title{i}",
                    Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. " +
                        "Itaque quibusdam ut ad molestiae labore ipsam distinctio, quam vitae sequi optio sit deserunt? " +
                        "Distinctio eveniet rem illo impedit, in dicta perspiciatis.",
                    Price = random.Next(2000, 20000) / 100,
                    Size = random.Next(1, 230),
                    ReleaseDate = DateTime.Now.AddMonths(-i),
                    CategoryId = categoryIds[random.Next(0, categoryIds.Count)],
                    VideoId = "pyZw_oqk7Q8"
                });
            }

            db.SaveChanges();
        }

        private static void SeedOrders(GameBoxDbContext db)
        {
            if (db.Orders.Any())
            {
                return;
            }

            List<User> users = db
                .Users
                .ToList();

            List<Game> games = db
                .Games
                .ToList();

            foreach (var user in users)
            {
                int ordersCount = random.Next(0, 4);

                for (int i = 0; i < ordersCount; i++)
                {
                    Order order = new Order();

                    int gamesCount = random.Next(1, 6);

                    for (int j = 0; j < gamesCount; j++)
                    {
                        Game game = games[random.Next(0, games.Count)];

                        if (order.Games.Any(g => g.GameId == game.Id))
                        {
                            j--;
                            continue;
                        }

                        order.Games.Add(new GameOrder
                        {
                            Game = game,
                            GameId = game.Id
                        });
                    }

                    order.TimeStamp = DateTime.Now.AddMonths(-i).AddDays(-i);
                    order.Price = order.Games.Sum(g => g.Game.Price);
                    user.Orders.Add(order);
                }
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