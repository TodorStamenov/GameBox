using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Wishlists.Queries.GetAllGames
{
    public class GetAllGamesQuery : IRequest<IEnumerable<Game>>
    {
        public class GetGamesInWishlistQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<Game>>
        {
            private readonly IDataService context;
            private readonly ICurrentUserService currentUser;

            public GetGamesInWishlistQueryHandler(
                IDataService context,
                ICurrentUserService currentUser)
            {
                this.context = context;
                this.currentUser = currentUser;
            }

            public async Task<IEnumerable<Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(this.context
                    .All<Wishlist>()
                    .Where(w => w.CustomerId == this.currentUser.CustomerId)
                    .Select(w => w.Game)
                    .OrderBy(g => g.Title)
                    .ToList());
            }
        }
    }
}
