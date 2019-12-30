using GameBox.Application.Games.Queries.GetAllGames;
using MediatR;
using System;
using System.Collections.Generic;

namespace GameBox.Application.Games.Queries.GetOwnedGames
{
    public class GetOwnedGamesQuery : IRequest<IEnumerable<GamesListViewModel>>
    {
        public Guid UserId { get; set; }

        public int LoadedGames { get; set; }
    }
}
