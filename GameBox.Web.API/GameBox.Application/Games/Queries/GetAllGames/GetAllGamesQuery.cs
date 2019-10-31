using MediatR;
using System.Collections.Generic;

namespace GameBox.Application.Games.Queries.GetAllGames
{
    public class GetAllGamesQuery : IRequest<IEnumerable<GamesListViewModel>>
    {
        public int LoadedGames { get; set; }
    }
}
