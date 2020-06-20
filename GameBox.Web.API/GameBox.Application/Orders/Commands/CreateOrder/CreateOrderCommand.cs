using GameBox.Application.Contracts;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            private readonly IGameBoxDbContext context;

            public CreateOrderCommandHandler(IMediator mediator, IDateTimeService dateTime, IGameBoxDbContext context)
            {
                this.mediator = mediator;
                this.dateTime = dateTime;
                this.context = context;
            }

            public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var games = await this.context
                    .Set<Game>()
                    .Where(g => request.GameIds.Contains(g.Id))
                    .ToListAsync(cancellationToken);

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

                await this.context.Set<Order>().AddAsync(order);
                await this.context.SaveChangesAsync(cancellationToken);

                var notification = new OrderCreated
                {
                    Username = request.Username,
                    Price = order.Price,
                    GamesCount = order.Games.Count,
                    TimeStamp = order.TimeStamp
                };

                await this.mediator.Publish(notification, cancellationToken);

                return string.Format(Constants.Common.Success, nameof(Order), string.Empty, Constants.Common.Added);
            }
        }
    }
}
