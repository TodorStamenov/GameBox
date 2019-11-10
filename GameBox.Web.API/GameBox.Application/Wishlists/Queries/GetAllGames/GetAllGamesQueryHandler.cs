using GameBox.Application.Contracts;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Wishlists.Queries.GetAllGames
{
    public class GetGamesInWishlistQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<Game>>
    {
        private readonly IMediator mediator;
        private readonly IGameBoxDbContext context;

        public GetGamesInWishlistQueryHandler(IMediator mediator, IGameBoxDbContext context)
        {
            this.mediator = mediator;
            this.context = context;
        }

        public async Task<IEnumerable<Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            return await this.context
                .Wishlists
                .Where(w => w.UserId == request.UserId)
                .Select(w => w.Game)
                .OrderBy(g => g.Title)
                .ToListAsync(cancellationToken);
        }
    }
}
