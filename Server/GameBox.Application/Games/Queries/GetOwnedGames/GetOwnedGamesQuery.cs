using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Games.Queries.GetAllGames;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetOwnedGames
{
    public class GetOwnedGamesQuery : IRequest<IEnumerable<GamesListViewModel>>
    {
        public Guid UserId { get; set; }

        public int LoadedGames { get; set; }

        public class GetOwnedGamesQueryHandler : IRequestHandler<GetOwnedGamesQuery, IEnumerable<GamesListViewModel>>
        {
            private const int GameCardsCount = 9;

            private readonly IMapper mapper;
            private readonly IDataService context;

            public GetOwnedGamesQueryHandler(IMapper mapper, IDataService context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<IEnumerable<GamesListViewModel>> Handle(GetOwnedGamesQuery request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(this.context
                    .All<User>()
                    .Where(u => u.Id == request.UserId)
                    .SelectMany(u => u.Orders
                        .OrderByDescending(o => o.TimeStamp)
                        .SelectMany(o => o.Games.Select(g => g.Game)))
                    .Distinct()
                    .OrderBy(g => g.Title)
                    .Skip(request.LoadedGames)
                    .Take(GameCardsCount)
                    .ProjectTo<GamesListViewModel>(this.mapper.ConfigurationProvider)
                    .ToList());
            }
        }
    }
}
