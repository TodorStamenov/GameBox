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
    public class CreateOrderCommand : IRequest<CreateOrderViewModel>
    {
        public string Username { get; set; }

        public IEnumerable<Guid> GameIds { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderViewModel>
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

            public async Task<CreateOrderViewModel> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var userId = await this.context
                    .Users
                    .Where(u => u.Username == request.Username)
                    .Select(u => u.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (userId == default(Guid))
                {
                    throw new NotFoundException(nameof(User), request.Username);
                }

                var games = await this.context
                    .Games
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
                    UserId = userId,
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

                await this.context.Orders.AddAsync(order);
                await this.context.SaveChangesAsync(cancellationToken);

                var notification = new OrderCreated
                {
                    Username = request.Username,
                    Price = order.Price,
                    GamesCount = order.Games.Count,
                    TimeStamp = order.TimeStamp
                };

                await this.mediator.Publish(notification, cancellationToken);

                return new CreateOrderViewModel
                {
                    Message = string.Format(Constants.Common.Success, nameof(Order), string.Empty, Constants.Common.Added)
                };
            }
        }
    }
}
