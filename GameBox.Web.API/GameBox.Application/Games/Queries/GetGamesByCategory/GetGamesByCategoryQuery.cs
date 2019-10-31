using GameBox.Application.Games.Queries.GetAllGames;
using MediatR;
using System;
using System.Collections.Generic;

namespace GameBox.Application.Games.Queries.GetGamesByCategory
{
    public class GetGamesByCategoryQuery : IRequest<IEnumerable<GamesListViewModel>>
    {
        public int LoadedGames { get; set; }

        public Guid CategoryId { get; set; }
    }
}
