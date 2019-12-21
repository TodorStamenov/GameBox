using GameBox.Application.Infrastructure;
using GameBox.Application.Orders.Commands.CreateOrder;
using GameBox.Domain.Entities;
using MediatR;
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

        private readonly GameBoxDbContext db;
        private readonly IMediator mediator;

        public GameBoxDbInitializer(GameBoxDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        public async Task SeedDatabaseAsync()
        {
            await this.db.Database.MigrateAsync();

            await this.SeedRolesAsync(Constants.Common.Admin);
            await this.SeedUsersAsync(UsersCount);
            await this.SeedUsersAsync(AdminsCount, Constants.Common.Admin);
            await this.SeedCategoriesAsync(CategoriesCount);
            await this.SeedGamesAsync(GamesCount);
            await this.SeedOrdersAsync();
            await this.SeedWishlistsAsync();
        }

        private async Task SeedRolesAsync(string roleName)
        {
            if (await this.db.Roles.AnyAsync())
            {
                return;
            }

            var role = new Role { Name = roleName };

            await this.db.Roles.AddAsync(role);
            await this.db.SaveChangesAsync();
        }

        private async Task SeedUsersAsync(int usersCount)
        {
            if (await this.db.Users.AnyAsync(u => !u.Roles.Any()))
            {
                return;
            }

            for (int i = 1; i <= usersCount; i++)
            {
                byte[] salt = this.GenerateSalt();

                var user = new User
                {
                    Username = $"User{i}",
                    Password = this.HashPassword("123", salt),
                    Salt = salt
                };

                await this.db.Users.AddAsync(user);
            }

            await this.db.SaveChangesAsync();
        }

        private async Task SeedUsersAsync(int usersCount, string role)
        {
            if (await this.db.Users.AnyAsync(u => u.Roles.Any(r => r.Role.Name == role)))
            {
                return;
            }

            Guid roleId = await this.db
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
                byte[] salt = this.GenerateSalt();

                var user = new User
                {
                    Username = $"{role}{i}",
                    Password = this.HashPassword("123", salt),
                    Salt = salt
                };

                var userRole = new UserRoles
                {
                    RoleId = roleId
                };

                user.Roles.Add(userRole);

                await this.db.Users.AddAsync(user);
            }

            await this.db.SaveChangesAsync();
        }

        private async Task SeedCategoriesAsync(int categoriesCount)
        {
            if (await this.db.Categories.AnyAsync())
            {
                return;
            }

            for (int i = 1; i <= categoriesCount; i++)
            {
                var category = new Category { Name = $"Category{i}" };

                await this.db.Categories.AddAsync(category);
            }

            await this.db.SaveChangesAsync();
        }

        private async Task SeedGamesAsync(int gamesCount)
        {
            if (await this.db.Games.AnyAsync())
            {
                return;
            }

            var categoryIds = await this.db
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

                await this.db.Games.AddAsync(game);
            }

            await this.db.SaveChangesAsync();
        }

        private async Task SeedOrdersAsync()
        {
            if (await this.db.Orders.AnyAsync())
            {
                return;
            }

            var users = await this.db.Users.ToListAsync();
            var games = await this.db.Games.ToListAsync();

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

            await this.db.SaveChangesAsync();

            var orders = this.db.Orders
                .Select(o => new OrderCreated
                {
                    Username = o.User.Username,
                    Price = o.Price,
                    GamesCount = o.Games.Count,
                    TimeStamp = o.TimeStamp
                });

            foreach (var order in orders)
            {
                await this.mediator.Publish(order);
            }
        }

        private async Task SeedWishlistsAsync()
        {
            if (await this.db.Wishlists.AnyAsync())
            {
                return;
            }

            var users = await this.db.Users.ToListAsync();
            var gameIds = await this.db.Games.Select(g => g.Id).ToListAsync();

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

            await this.db.SaveChangesAsync();
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private string HashPassword(string password, byte[] salt)
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
