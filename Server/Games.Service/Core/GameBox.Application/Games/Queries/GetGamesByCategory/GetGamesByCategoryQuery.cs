using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Games.Queries.GetAllGames;
using GameBox.Application.Infrastructure;
using GameBox.Application.Infrastructure.Extensions;
using GameBox.Application.Model;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace GameBox.Application.Games.Queries.GetGamesByCategory;

public class GetGamesByCategoryQuery : IRequest<IEnumerable<GamesListViewModel>>
{
    public int LoadedGames { get; set; }

    public Guid CategoryId { get; set; }

    public class GetGamesByCategoryQueryHandler : IRequestHandler<GetGamesByCategoryQuery, IEnumerable<GamesListViewModel>>
    {
        private const int GameCardsCount = 9;

        private readonly IMapper mapper;
        private readonly IDataService context;
        private readonly IDistributedCache cache;

        public GetGamesByCategoryQueryHandler(
            IMapper mapper,
            IDataService context,
            IDistributedCache cache)
        {
            this.mapper = mapper;
            this.context = context;
            this.cache = cache;
        }

        public async Task<IEnumerable<GamesListViewModel>> Handle(GetGamesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var games = await this.cache
                .GetRecordAsync<IEnumerable<GamesCacheModel>>(Constants.Caching.RedisGamesKey);

            if (games is null || !games.Any())
            {
                return this.context
                    .All<Game>()
                    .Where(g => g.CategoryId == request.CategoryId)
                    .Skip(request.LoadedGames)
                    .Take(GameCardsCount)
                    .ProjectTo<GamesListViewModel>(this.mapper.ConfigurationProvider)
                    .ToList();
            }
            
            return games
                .Where(g => g.CategoryId == request.CategoryId)
                .Skip(request.LoadedGames)
                .Take(GameCardsCount)
                .AsQueryable()
                .ProjectTo<GamesListViewModel>(this.mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
