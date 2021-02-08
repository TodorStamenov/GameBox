using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Wishlists.Queries.GetAllGames
{
    public class GetAllGamesQuery : IRequest<IEnumerable<Game>>
    {
        public Guid UserId { get; set; }

        public class GetGamesInWishlistQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<Game>>
        {
            private readonly IDataService context;

            public GetGamesInWishlistQueryHandler(IDataService context)
            {
                this.context = context;
            }

            public async Task<IEnumerable<Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
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

                return await Task.FromResult(this.context
                    .All<Wishlist>()
                    .Where(w => w.UserId == customerId)
                    .Select(w => w.Game)
                    .OrderBy(g => g.Title)
                    .ToList());
            }
        }
    }
}
