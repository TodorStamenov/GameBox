using AutoMapper;
using GameBox.Application.Contracts;
using GameBox.Application.Games.Queries.GetAllGames;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetGamesByCategory
{
    public class GetGamesByCategoryQueryHandler : IRequestHandler<GetGamesByCategoryQuery, IEnumerable<GamesListViewModel>>
    {
        private const int GameCardsCount = 9;

        private readonly IMapper mapper;
        private readonly IGameBoxDbContext context;

        public GetGamesByCategoryQueryHandler(IMapper mapper, IGameBoxDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<GamesListViewModel>> Handle(GetGamesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var games = await this.context
                .Games
                .Where(g => g.CategoryId == request.CategoryId)
                .OrderByDescending(g => g.ReleaseDate)
                .ThenByDescending(g => g.ViewCount)
                .ThenBy(g => g.Title)
                .Skip(request.LoadedGames)
                .Take(GameCardsCount)
                .ToListAsync(cancellationToken);

            return this.mapper.Map<IEnumerable<GamesListViewModel>>(games);
        }
    }
}
