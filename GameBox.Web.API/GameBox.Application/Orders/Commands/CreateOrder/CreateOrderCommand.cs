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

namespace GameBox.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<string>
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public IEnumerable<Guid> GameIds { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
        {
            private readonly IMediator mediator;
            private readonly IDateTimeService dateTime;
            private readonly IDataService context;
            private readonly IMessageQueueSenderService queue;

            public CreateOrderCommandHandler(
                IMediator mediator,
                IDateTimeService dateTime,
                IDataService context,
                IMessageQueueSenderService queue)
            {
                this.mediator = mediator;
                this.dateTime = dateTime;
                this.context = context;
                this.queue = queue;
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
                    UserId = request.UserId,
                    TimeStamp = this.dateTime.UtcNow,
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
                    TimeStamp = order.TimeStamp
                };

                var queueName = "orders";
                var message = new Message(queueName, messageData);

                await this.context.SaveAsync(cancellationToken, message);
                this.queue.Send(queueName, messageData);
                await this.context.MarkMessageAsPublished(message.Id);

                return string.Format(Constants.Common.Success, nameof(Order), string.Empty, Constants.Common.Added);
            }
        }
    }
}
