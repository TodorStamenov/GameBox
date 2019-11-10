using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Wishlists.Commands.RemoveGame
{
    public class RemoveGameCommandHandler : IRequestHandler<RemoveGameCommand, Guid>
    {
        private readonly IGameBoxDbContext context;

        public RemoveGameCommandHandler(IGameBoxDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> Handle(RemoveGameCommand request, CancellationToken cancellationToken)
        {
            var wishlist = await this.context.Wishlists.FindAsync(request.UserId, request.GameId);

            if (wishlist == null)
            {
                throw new NotFoundException(nameof(Game), request.GameId);
            }

            this.context.Wishlists.Remove(wishlist);
            await this.context.SaveChangesAsync(cancellationToken);

            return request.GameId;
        }
    }
}
