using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Wishlists.Commands.RemoveGame
{
    public class RemoveGameCommand : IRequest<Guid>
    {
        public Guid GameId { get; set; }

        public class RemoveGameCommandHandler : IRequestHandler<RemoveGameCommand, Guid>
        {
            private readonly IDataService context;
            private readonly ICurrentUserService currentUser;

            public RemoveGameCommandHandler(
                IDataService context,
                ICurrentUserService currentUser)
            {
                this.context = context;
                this.currentUser = currentUser;
            }

            public async Task<Guid> Handle(RemoveGameCommand request, CancellationToken cancellationToken)
            {
                var wishlist = this.context
                    .All<Wishlist>()
                    .Where(w => w.CustomerId == this.currentUser.CustomerId)
                    .Where(w => w.GameId == request.GameId)
                    .FirstOrDefault();

                if (wishlist == null)
                {
                    throw new NotFoundException(nameof(Game), request.GameId);
                }

                await this.context.DeleteAsync(wishlist);
                await this.context.SaveAsync(cancellationToken);

                return request.GameId;
            }
        }
    }
}
