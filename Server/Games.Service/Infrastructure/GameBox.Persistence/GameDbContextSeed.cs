using GameBox.Application.Contracts.Services;
using GameBox.Application.Orders.Commands.CreateOrder;
using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameBox.Persistence
{
    public static class GameDbContextSeed
    {
        private const int CategoriesCount = 7;
        private const int GamesCount = CategoriesCount * 10;

        private static GameDbContext context;
        private static IQueueSenderService messageQueue;

        private static readonly Random random = new Random();

        public static async Task SeedDatabaseAsync(
            GameDbContext database,
            IQueueSenderService serviceBus)
        {
            context = database;
            messageQueue = serviceBus;

            await SeedCategoriesAsync(CategoriesCount);
            await SeedGamesAsync(GamesCount);
            await SeedOrdersAsync();
            await SeedWishlistsAsync();
            await SeedCommentsAsync();
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

            var users = await context.Customers.ToListAsync();
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

                    order.DateAdded = DateTime.Now.AddMonths(-i).AddDays(-i);
                    order.Price = order.Games.Sum(g => g.Game.Price);
                    user.Orders.Add(order);
                }
            }

            await context.SaveChangesAsync();

            var orders = await context.Orders
                .Select(o => new OrderCreatedMessage
                {
                    Username = o.User.Username,
                    Price = o.Price,
                    GamesCount = o.Games.Count,
                    TimeStamp = o.DateAdded
                })
                .ToListAsync();

            foreach (var order in orders)
            {
                messageQueue.PostQueueMessage(queueName: "orders", order);
            }
        }

        private static async Task SeedWishlistsAsync()
        {
            if (await context.Wishlists.AnyAsync())
            {
                return;
            }

            var users = await context.Customers.ToListAsync();
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
            var userIds = await context.Customers.Select(u => u.Id).ToListAsync();

            if (!games.Any() || !userIds.Any())
            {
                return;
            }

            foreach (var game in games)
            {
                var commentsCount = random.Next(0, 9);

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
                        DateAdded = DateTime.Now.AddMonths(-i).AddDays(-i),
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
