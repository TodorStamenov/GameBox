using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Infrastructure;
using GameBox.Application.Infrastructure.Extensions;
using GameBox.Application.Model;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
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
            private readonly IDistributedCache cache;

            public GetAllGamesQueryHandler(
                IMapper mapper,
                IDataService context,
                IDistributedCache cache)
            {
                this.mapper = mapper;
                this.context = context;
                this.cache = cache;
            }

            public async Task<IEnumerable<GamesListViewModel>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
            {
                var games = await this.cache
                    .GetRecordAsync<IEnumerable<GamesCacheModel>>(Constants.Caching.RedisGamesKey);

                if (games is null || !games.Any())
                {
                    return this.context
                        .All<Game>()
                        .Skip(request.LoadedGames)
                        .Take(GameCardsCount)
                        .ProjectTo<GamesListViewModel>(this.mapper.ConfigurationProvider)
                        .ToList();
                }

                return games
                    .Skip(request.LoadedGames)
                    .Take(GameCardsCount)
                    .AsQueryable()
                    .ProjectTo<GamesListViewModel>(this.mapper.ConfigurationProvider)
                    .ToList();
            }
        }
    }
}
