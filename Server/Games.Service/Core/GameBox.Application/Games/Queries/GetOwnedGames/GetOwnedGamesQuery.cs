using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Games.Queries.GetAllGames;
using GameBox.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetOwnedGames
{
    public class GetOwnedGamesQuery : IRequest<IEnumerable<GamesListViewModel>>
    {
        public int LoadedGames { get; set; }

        public class GetOwnedGamesQueryHandler : IRequestHandler<GetOwnedGamesQuery, IEnumerable<GamesListViewModel>>
        {
            private const int GameCardsCount = 9;

            private readonly IMapper mapper;
            private readonly IDataService context;
            private readonly ICurrentUserService currentUser;

            public GetOwnedGamesQueryHandler(
                IMapper mapper,
                IDataService context,
                ICurrentUserService currentUser)
            {
                this.mapper = mapper;
                this.context = context;
                this.currentUser = currentUser;
            }

            public async Task<IEnumerable<GamesListViewModel>> Handle(GetOwnedGamesQuery request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(this.context
                    .All<Order>()
                    .Where(o => o.UserId == this.currentUser.CustomerId)
                    .OrderByDescending(o => o.TimeStamp)
                    .SelectMany(o => o.Games.Select(g => g.Game))
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
