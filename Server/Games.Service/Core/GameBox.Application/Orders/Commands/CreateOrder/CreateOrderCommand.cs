using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<string>
{
    public string Username { get; set; }

    public IEnumerable<Guid> GameIds { get; set; }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly IDateTimeService dateTime;
        private readonly IDataService context;
        private readonly IQueueSenderService queue;
        private readonly ICurrentUserService currentUser;

        public CreateOrderCommandHandler(
            IDateTimeService dateTime,
            IDataService context,
            IQueueSenderService queue,
            ICurrentUserService currentUser)
        {
            this.dateTime = dateTime;
            this.context = context;
            this.queue = queue;
            this.currentUser = currentUser;
        }

        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var games = this.context
                .All<Game>()
                .Where(g => request.GameIds.Contains(g.Id))
                .ToList();

            if (!games.Any())
            {
                throw new NotFoundException(nameof(Game), request.GameIds.First());
            }

            foreach (var game in games)
            {
                game.OrderCount++;
            }

            var order = new Order
            {
                CustomerId = this.currentUser.CustomerId,
                DateAdded = this.dateTime.UtcNow,
                Price = games.Sum(g => g.Price)
            };

            foreach (var game in games)
            {
                order.Games.Add(new GameOrder
                {
                    GameId = game.Id
                });
            }

            await this.context.AddAsync(order);

            var messageData = new OrderCreatedMessage
            {
                Username = request.Username,
                Price = order.Price,
                GamesCount = order.Games.Count,
                TimeStamp = order.DateAdded,
                Games = order.Games
                    .Select(g => new OrderGame
                    {
                        Id = g.GameId,
                        Title = g.Game.Title,
                        Price = g.Game.Price,
                        ViewCount = g.Game.ViewCount,
                        OrderCount = g.Game.OrderCount
                    })
                    .ToList()
            };

            var queueName = "orders";
            var messageId = await this.context.SaveAsync(queueName, messageData, cancellationToken);

            this.queue.PostQueueMessage(queueName, messageData);
            await this.context.MarkMessageAsPublished(messageId);

            return string.Format(Constants.Common.Success, nameof(Order), string.Empty, Constants.Common.Added);
        }
    }
}
