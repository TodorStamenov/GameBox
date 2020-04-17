using GameBox.Application.Contracts;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Wishlists.Commands.AddGame
{
    public class AddGameCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        public class AddGameCommandHandler : IRequestHandler<AddGameCommand, Guid>
        {
            private readonly IGameBoxDbContext context;

            public AddGameCommandHandler(IGameBoxDbContext context)
            {
                this.context = context;
            }

            public async Task<Guid> Handle(AddGameCommand request, CancellationToken cancellationToken)
            {
                var gameExists = await this.context
                    .Wishlists
                    .AnyAsync(w => w.UserId == request.UserId && w.GameId == request.GameId);

                if (gameExists)
                {
                    return request.GameId;
                }

                var wishlist = new Wishlist
                {
                    UserId = request.UserId,
                    GameId = request.GameId
                };

                await this.context.Wishlists.AddAsync(wishlist);
                await this.context.SaveChangesAsync(cancellationToken);

                return request.GameId;
            }
        }
    }
}
