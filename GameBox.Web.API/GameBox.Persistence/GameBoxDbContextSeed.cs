using GameBox.Application.Contracts.Services;
using GameBox.Application.Infrastructure;
using GameBox.Application.Orders.Commands.CreateOrder;
using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameBox.Persistence
{
    public static class GameBoxDbContextSeed
    {
        private const int AdminsCount = 1;
        private const int UsersCount = 50;
        private const int CategoriesCount = 7;
        private const int GamesCount = CategoriesCount * 10;

        private static GameBoxDbContext context;
        private static IAccountService accountService;
        private static IMessageQueueSenderService messageQueue;

        private static readonly Random random = new Random();

        public static async Task SeedDatabaseAsync(
            GameBoxDbContext database,
            IAccountService account,
            IMessageQueueSenderService serviceBus)
        {
            context = database;
            accountService = account;
            messageQueue = serviceBus;

            await SeedRolesAsync(Constants.Common.Admin);
            await SeedUsersAsync(UsersCount);
            await SeedUsersAsync(AdminsCount, Constants.Common.Admin);
            await SeedCategoriesAsync(CategoriesCount);
            await SeedGamesAsync(GamesCount);
            await SeedOrdersAsync();
            await SeedWishlistsAsync();
            await SeedCommentsAsync();
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
                byte[] salt = accountService.GenerateSalt();

                var user = new User
                {
                    Username = $"User{i}",
                    Password = accountService.HashPassword("123", salt),
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

            if (roleId == default(Guid))
            {
                return;
            }

            for (int i = 1; i <= usersCount; i++)
            {
                byte[] salt = accountService.GenerateSalt();

                var user = new User
                {
                    Username = $"{role}{i}",
                    Password = accountService.HashPassword("123", salt),
                    Salt = salt
                };

                var userRole = new UserRoles
                {
                    RoleId = roleId
                };

                user.Roles.Add(userRole);

                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedCategoriesAsync(int categoriesCount)
        {
            if (await context.Categories.AnyAsync())
            {
                return;
            }

            for (int i = 1; i <= categoriesCount; i++)
            {
                var category = new Category { Name = $"Category{i}" };

                await context.Categories.AddAsync(category);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedGamesAsync(int gamesCount)
        {
            if (await context.Games.AnyAsync())
            {
                return;
            }

            var categoryIds = await context
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

                await context.Games.AddAsync(game);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedOrdersAsync()
        {
            if (await context.Orders.AnyAsync())
            {
                return;
            }

            var users = await context.Users.ToListAsync();
            var games = await context.Games.ToListAsync();

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

            await context.SaveChangesAsync();

            var orders = context.Orders
                .Select(o => new OrderCreated
                {
                    Username = o.User.Username,
                    Price = o.Price,
                    GamesCount = o.Games.Count,
                    TimeStamp = o.TimeStamp
                });

            foreach (var order in orders)
            {
                var command = new OrderCreated 
                {
                    Username = order.Username,
                    Price = order.Price,
                    GamesCount = order.GamesCount,
                    TimeStamp = order.TimeStamp
                };

                messageQueue.Send(queueName: "orders", command);
            }
        }

        private static async Task SeedWishlistsAsync()
        {
            if (await context.Wishlists.AnyAsync())
            {
                return;
            }

            var users = await context.Users.ToListAsync();
            var gameIds = await context.Games.Select(g => g.Id).ToListAsync();

            foreach (var user in users)
            {
                var itemsCount = random.Next(0, 6);

                for (int i = 0; i < itemsCount; i++)
                {
                    var gameId = gameIds[random.Next(0, gameIds.Count)];

                    if (user.Wishlist.Any(w => w.GameId == gameId))
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

            await context.SaveChangesAsync();
        }

        private static async Task SeedCommentsAsync()
        {
            if (await context.Comments.AnyAsync())
            {
                return;
            }

            var games = await context.Games.ToListAsync();
            var userIds = await context.Users.Select(u => u.Id).ToListAsync();

            foreach (var game in games)
            {
                var commentsCount = random.Next(0, 4);

                for (int i = 0; i < commentsCount; i++)
                {
                    var userId = userIds[random.Next(0, userIds.Count)];

                    if (game.Comments.Any(c => c.UserId == userId))
                    {
                        i--;
                        continue;
                    }

                    var comment = new Comment
                    {
                        GameId = game.Id,
                        UserId = userId,
                        TimeStamp = DateTime.Now.AddMonths(-i).AddDays(-i),
                        Content = "Comment lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                            "Suspendisse tellus ipsum, dignissim sit amet rutrum congue, vehicula in elit. " +
                            "Phasellus lorem urna, iaculis non egestas eu, sollicitudin nec risus. Nunc mollis nisi a orci vulputate molestie. " +
                            "Nam hendrerit ex sit amet ligula sollicitudin, at placerat sapien auctor. " +
                            "Suspendisse pulvinar imperdiet quam. Proin pellentesque efficitur dui. " +
                            "Suspendisse commodo aliquam elit, consequat pellentesque ipsum dictum sed."
                    };

                    game.Comments.Add(comment);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
