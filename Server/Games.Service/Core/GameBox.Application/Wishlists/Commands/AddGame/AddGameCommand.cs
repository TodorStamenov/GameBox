using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Wishlists.Commands.AddGame
{
    public class AddGameCommand : IRequest<Guid>
    {
        public Guid GameId { get; set; }

        public class AddGameCommandHandler : IRequestHandler<AddGameCommand, Guid>
        {
            private readonly IDataService context;
            private readonly ICurrentUserService currentUser;

            public AddGameCommandHandler(
                IDataService context,
                ICurrentUserService currentUser)
            {
                this.context = context;
                this.currentUser = currentUser;
            }

            public async Task<Guid> Handle(AddGameCommand request, CancellationToken cancellationToken)
            {                
                var gameExists = this.context
                    .All<Wishlist>()
                    .Any(w => w.UserId == this.currentUser.CustomerId && w.GameId == request.GameId);

                if (gameExists)
                {
                    return request.GameId;
                }

                var wishlist = new Wishlist
                {
                    GameId = request.GameId,
                    UserId = this.currentUser.CustomerId
                };

                await this.context.AddAsync(wishlist);
                await this.context.SaveAsync(cancellationToken);

                return request.GameId;
            }
        }
    }
}
