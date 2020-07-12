using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetAllGames
{
    public class GetAllGamesQuery : IRequest<IEnumerable<GamesListViewModel>>
    {
        public int LoadedGames { get; set; }

        public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<GamesListViewModel>>
        {
            private const int GameCardsCount = 9;

            private readonly IMapper mapper;
            private readonly IDataService context;

            public GetAllGamesQueryHandler(IMapper mapper, IDataService context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<IEnumerable<GamesListViewModel>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(this.context
                    .All<Game>()
                    .OrderByDescending(g => g.ReleaseDate)
                    .ThenByDescending(g => g.ViewCount)
                    .ThenBy(g => g.Title)
                    .Skip(request.LoadedGames)
                    .Take(GameCardsCount)
                    .ProjectTo<GamesListViewModel>(this.mapper.ConfigurationProvider)
                    .ToList());
            }
        }
    }
}
