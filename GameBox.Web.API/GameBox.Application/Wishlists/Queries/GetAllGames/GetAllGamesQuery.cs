using GameBox.Application.Contracts;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            private readonly IGameBoxDbContext context;

            public GetGamesInWishlistQueryHandler(IGameBoxDbContext context)
            {
                this.context = context;
            }

            public async Task<IEnumerable<Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
            {
                return await this.context
                    .Set<Wishlist>()
                    .Where(w => w.UserId == request.UserId)
                    .Select(w => w.Game)
                    .OrderBy(g => g.Title)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
