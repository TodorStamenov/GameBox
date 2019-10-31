using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts;
using GameBox.Application.Games.Queries.GetAllGames;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetOwnedGames
{
    public class GetOwnedGamesQueryHandler : IRequestHandler<GetOwnedGamesQuery, IEnumerable<GamesListViewModel>>
    {
        private const int GameCardsCount = 9;

        private readonly IMapper mapper;
        private readonly IGameBoxDbContext context;

        public GetOwnedGamesQueryHandler(IMapper mapper, IGameBoxDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<GamesListViewModel>> Handle(GetOwnedGamesQuery request, CancellationToken cancellationToken)
        {
            return await this.context
                .Users
                .Where(u => u.Username == request.Username)
                .SelectMany(u => u.Orders.SelectMany(o => o.Games.Select(g => g.Game)))
                .Distinct()
                .OrderBy(g => g.Title)
                .Skip(request.LoadedGames)
                .Take(GameCardsCount)
                .ProjectTo<GamesListViewModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
