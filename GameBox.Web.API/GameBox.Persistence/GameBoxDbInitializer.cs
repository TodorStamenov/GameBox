using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GameBox.Persistence
{
    public class GameBoxDbInitializer
    {
        private const int AdminsCount = 1;
        private const int UsersCount = 50;
        private const int CategoriesCount = 7;
        private const int GamesCount = CategoriesCount * 10;

        private static readonly Random random = new Random();

        public static async Task SeedDatabaseAsync(GameBoxDbContext db)
        {
            await db.Database.EnsureCreatedAsync();

            await SeedRolesAsync(db, Constants.Common.Admin);
            await SeedUsersAsync(db, UsersCount);
            await SeedUsersAsync(db, AdminsCount, Constants.Common.Admin);
            await SeedCategoriesAsync(db, CategoriesCount);
            await SeedGamesAsync(db, GamesCount);
            await SeedOrdersAsync(db);
            await SeedWishlistsAsync(db);
        }

        private static async Task SeedRolesAsync(GameBoxDbContext db, string roleName)
        {
            if (await db.Roles.AnyAsync())
            {
                return;
            }

            var role = new Role { Name = roleName };

            await db.Roles.AddAsync(role);
            await db.SaveChangesAsync();
        }

        private static async Task SeedUsersAsync(GameBoxDbContext db, int usersCount)
        {
            if (await db.Users.AnyAsync(u => !u.Roles.Any()))
            {
                return;
            }

            for (int i = 1; i <= usersCount; i++)
            {
                byte[] salt = GenerateSalt();

                var user = new User
                {
                    Username = $"User{i}",
                    Password = HashPassword("123", salt),
                    Salt = salt
                };

                await db.Users.AddAsync(user);
            }

            await db.SaveChangesAsync();
        }

        private static async Task SeedUsersAsync(GameBoxDbContext db, int usersCount, string role)
        {
            if (await db.Users.AnyAsync(u => u.Roles.Any(r => r.Role.Name == role)))
            {
                return;
            }

            Guid roleId = await db
                .Roles
                .Where(r => r.Name == role)
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            if (roleId == default(Guid))
            {
                return;
            }

            for (int i = 1; i <= usersCount; i++)
            {
                byte[] salt = GenerateSalt();

                var user = new User
                {
                    Username = $"{role}{i}",
                    Password = HashPassword("123", salt),
                    Salt = salt
                };

                var userRole = new UserRoles
                {
                    RoleId = roleId
                };

                user.Roles.Add(userRole);

                await db.Users.AddAsync(user);
            }

            await db.SaveChangesAsync();
        }

        private static async Task SeedCategoriesAsync(GameBoxDbContext db, int categoriesCount)
        {
            if (await db.Categories.AnyAsync())
            {
                return;
            }

            for (int i = 1; i <= categoriesCount; i++)
            {
                var category = new Category { Name = $"Category{i}" };

                await db.Categories.AddAsync(category);
            }

            await db.SaveChangesAsync();
        }

        private static async Task SeedGamesAsync(GameBoxDbContext db, int gamesCount)
        {
            if (await db.Games.AnyAsync())
            {
                return;
            }

            var categoryIds = await db
                .Categories
                .Select(c => c.Id)
                .ToListAsync();

            for (int i = 1; i <= gamesCount; i++)
            {
                var game = new Game
                {
                    Title = $"Title{i}",
                    Price = random.Next(2000, 20000) / 100M,
                    Size = random.Next(1, 230),
                    ReleaseDate = DateTime.Now.AddMonths(-i),
                    CategoryId = categoryIds[random.Next(0, categoryIds.Count)],
                    VideoId = "pyZw_oqk7Q8",
                    ViewCount = random.Next(10, 10000),
                    OrderCount = random.Next(10, 10000),
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Suspendisse tellus ipsum, dignissim sit amet rutrum congue, vehicula in elit. " +
                        "Phasellus lorem urna, iaculis non egestas eu, sollicitudin nec risus. Nunc mollis nisi a orci vulputate molestie. " +
                        "Nam hendrerit ex sit amet ligula sollicitudin, at placerat sapien auctor. " +
                        "Suspendisse pulvinar imperdiet quam. Proin pellentesque efficitur dui. " +
                        "Suspendisse commodo aliquam elit, consequat pellentesque ipsum dictum sed. " +
                        "Nam vel nisi et risus porttitor ultrices vestibulum ut ante. Duis vulputate interdum bibendum. " +
                        "Etiam vitae elit eget leo interdum varius. Aenean volutpat vehicula felis, a semper nisl rutrum id. " +
                        "Donec ac fringilla nisl. Ut nec laoreet nunc, sit amet porttitor felis. Praesent in placerat libero. " +
                        "In ipsum mauris, dictum eu magna sed, tristique auctor lorem. " +
                        "Integer aliquet augue tellus, ac eleifend elit condimentum sed.Morbi luctus non lacus id facilisis.Vivamus vulputate elementum arcu, " +
                        "gravida malesuada ante facilisis eu.Vivamus in tellus ac urna tempor porta ut ac risus.Ut commodo ac arcu sed tincidunt.Mauris pharetra " +
                        "lectus at massa convallis fringilla.Morbi commodo ex enim, nec interdum nisl viverra id."
                };

                await db.Games.AddAsync(game);
            }

            await db.SaveChangesAsync();
        }

        private static async Task SeedOrdersAsync(GameBoxDbContext db)
        {
            if (await db.Orders.AnyAsync())
            {
                return;
            }

            var users = await db.Users.ToListAsync();
            var games = await db.Games.ToListAsync();

            foreach (var user in users)
            {
                var ordersCount = random.Next(0, 4);

                for (int i = 0; i < ordersCount; i++)
                {
                    var order = new Order();

                    var gamesCount = random.Next(1, 6);

                    for (int j = 0; j < gamesCount; j++)
                    {
                        Game game = games[random.Next(0, games.Count)];

                        if (order.Games.Any(g => g.GameId == game.Id))
                        {
                            j--;
                            continue;
                        }

                        var gameOrder = new GameOrder
                        {
                            Game = game,
                            GameId = game.Id
                        };

                        order.Games.Add(gameOrder);
                    }

                    order.TimeStamp = DateTime.Now.AddMonths(-i).AddDays(-i);
                    order.Price = order.Games.Sum(g => g.Game.Price);
                    user.Orders.Add(order);
                }
            }

            await db.SaveChangesAsync();
        }

        private static async Task SeedWishlistsAsync(GameBoxDbContext db)
        {
            if (await db.Wishlists.AnyAsync())
            {
                return;
            }

            var users = await db.Users.ToListAsync();
            var gameIds = await db.Games.Select(g => g.Id).ToListAsync();

            foreach (var user in users)
            {
                var itemsCount = random.Next(0, 6);

                for (int i = 0; i < itemsCount; i++)
                {
                    var gameId = gameIds[random.Next(0, gameIds.Count)];

                    if (user.Wishlist.Any(g => g.GameId == gameId))
                    {
                        i--;
                        continue;
                    }

                    var wishlistItem = new Wishlist 
                    {
                        UserId = user.Id,
                        GameId = gameId
                    };

                    user.Wishlist.Add(wishlistItem);
                }
            }

            await db.SaveChangesAsync();
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
