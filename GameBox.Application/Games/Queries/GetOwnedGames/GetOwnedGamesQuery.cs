using GameBox.Application.Games.Queries.GetAllGames;
using MediatR;
using System.Collections.Generic;

namespace GameBox.Application.Games.Queries.GetOwnedGames
{
    public class GetOwnedGamesQuery : IRequest<IEnumerable<GamesListViewModel>>
    {
        public string Username { get; set; }

        public int LoadedGames { get; set; }
    }
}
