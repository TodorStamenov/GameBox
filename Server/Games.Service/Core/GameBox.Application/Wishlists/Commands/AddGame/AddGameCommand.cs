using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
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
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        public class AddGameCommandHandler : IRequestHandler<AddGameCommand, Guid>
        {
            private readonly IDataService context;

            public AddGameCommandHandler(IDataService context)
            {
                this.context = context;
            }

            public async Task<Guid> Handle(AddGameCommand request, CancellationToken cancellationToken)
            {
                var customerId = this.context
                    .All<Customer>()
                    .Where(c => c.UserId == request.UserId)
                    .Select(c => c.Id)
                    .FirstOrDefault();

                if (customerId == default(Guid))
                {
                    throw new NotFoundException(nameof(Customer), request.UserId);
                }
                
                var gameExists = this.context
                    .All<Wishlist>()
                    .Any(w => w.UserId == customerId && w.GameId == request.GameId);

                if (gameExists)
                {
                    return request.GameId;
                }

                var wishlist = new Wishlist
                {
                    UserId = customerId,
                    GameId = request.GameId
                };

                await this.context.AddAsync(wishlist);
                await this.context.SaveAsync(cancellationToken);

                return request.GameId;
            }
        }
    }
}
