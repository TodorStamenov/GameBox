using GameBox.Application.Contracts.Services;
using GameBox.Application.Orders.Commands.CreateOrder;
using GameBox.Domain.Entities;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameBox.Persistence
{
    public class GameDbContextSeedService : GamesSeeder.GamesSeederBase
    {
        private const int CategoriesCount = 7;
        private const int GamesCount = CategoriesCount * 10;

        private readonly GameDbContext context;
        private readonly IQueueSenderService messageQueue;

        private static readonly Random random = new Random();

        public GameDbContextSeedService(
            GameDbContext context,
            IQueueSenderService messageQueue)
        {
            this.context = context;
            this.messageQueue = messageQueue;
        }

        public override async Task<SeedGamesReply> SeedGamesDatabase(SeedGamesRequest request, ServerCallContext context)
        {
            await this.SeedCategoriesAsync(CategoriesCount);
            await this.SeedGamesAsync(GamesCount);
            await this.SeedOrdersAsync();
            await this.SeedWishlistsAsync();
            await this.SeedCommentsAsync();

            return new SeedGamesReply { Seeded = true };
        }

        private async Task SeedCategoriesAsync(int categoriesCount)
        {
            if (await this.context.Categories.AnyAsync())
            {
                return;
            }

            for (int i = 1; i <= categoriesCount; i++)
            {
                var category = new Category { Name = $"Category{i}" };
                await this.context.Categories.AddAsync(category);
            }

            await this.context.SaveChangesAsync();
        }

        private async Task SeedGamesAsync(int gamesCount)
        {
            if (await this.context.Games.AnyAsync())
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

                await this.context.Games.AddAsync(game);
            }

            await this.context.SaveChangesAsync();
        }

        private async Task SeedOrdersAsync()
        {
            if (await this.context.Orders.AnyAsync())
            {
                return;
            }

            var users = await this.context.Customers.ToListAsync();
            var games = await this.context.Games.ToListAsync();

            foreach (var user in users)
            {
                var ordersCount = random.Next(0, 4);
                for (int i = 0; i < ordersCount; i++)
                {
                    var order = new Order();
                    var gamesCount = random.Next(1, 6);

                    for (int j = 0; j < gamesCount; j++)
                    {
                        var game = games[random.Next(0, games.Count)];
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

            await this.context.SaveChangesAsync();

            var orders = await this.context
                .Orders
                .Select(o => new OrderCreatedMessage
                {
                    Username = o.Customer.Username,
                    Price = o.Price,
                    GamesCount = o.Games.Count,
                    TimeStamp = o.DateAdded,
                    Games = o.Games
                        .Select(g => new OrderGame
                        {
                            Id = g.GameId,
                            Title = g.Game.Title,
                            Price = g.Game.Price,
                            ViewCount = g.Game.ViewCount,
                            OrderCount = g.Game.OrderCount
                        })
                        .ToList()
                })
                .ToListAsync();

            foreach (var order in orders)
            {
                this.messageQueue.PostQueueMessage(queueName: "orders", order);
            }
        }

        private async Task SeedWishlistsAsync()
        {
            if (await this.context.Wishlists.AnyAsync())
            {
                return;
            }

            var users = await this.context.Customers.ToListAsync();
            var gameIds = await this.context
                .Games
                .Select(g => g.Id)
                .ToListAsync();

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
                        CustomerId = user.Id,
                        GameId = gameId
                    };

                    user.Wishlist.Add(wishlistItem);
                }
            }

            await this.context.SaveChangesAsync();
        }

        private async Task SeedCommentsAsync()
        {
            if (await this.context.Comments.AnyAsync())
            {
                return;
            }

            var games = await this.context.Games.ToListAsync();
            var userIds = await this.context
                .Customers
                .Select(u => u.Id)
                .ToListAsync();

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
                    if (game.Comments.Any(c => c.CustomerId == userId))
                    {
                        i--;
                        continue;
                    }

                    var comment = new Comment
                    {
                        GameId = game.Id,
                        CustomerId = userId,
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

            await this.context.SaveChangesAsync();
        }
    }
}
