using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Games.Queries.GetAllGames;
using GameBox.Application.Infrastructure;
using GameBox.Application.Infrastructure.Extensions;
using GameBox.Application.Model;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetGamesByCategory
{
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
                var games = await this.cache.GetRecordAsync<IEnumerable<GamesCacheModel>>(Constants.Caching.RedisGamesKey);

                if (games is null || !games.Any())
                {
                    return Enumerable.Empty<GamesListViewModel>();
                }
                
                return games
                    .Where(g => g.CategoryId == request.CategoryId)
                    .Skip(request.LoadedGames)
                    .Take(GameCardsCount)
                    .Select(g => new GamesListViewModel
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Description = g.Description,
                        Price = g.Price,
                        Size = g.Size,
                        ThumbnailUrl = g.ThumbnailUrl,
                        VideoId = g.VideoId,
                        ViewCount = g.ViewCount
                    })
                    .ToList();
            }
        }
    }
}
