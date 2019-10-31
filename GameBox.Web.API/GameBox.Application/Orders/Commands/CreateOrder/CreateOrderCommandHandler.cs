using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly IGameBoxDbContext context;

        public CreateOrderCommandHandler(IGameBoxDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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
                TimeStamp = DateTime.Now,
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

            return string.Format(Constants.Common.Success, nameof(Order), string.Empty, Constants.Common.Added);
        }
    }
}
