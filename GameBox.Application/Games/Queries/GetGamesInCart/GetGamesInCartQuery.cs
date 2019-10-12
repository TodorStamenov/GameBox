using MediatR;
using System;
using System.Collections.Generic;

namespace GameBox.Application.Games.Queries.GetGamesInCart
{
    public class GetGamesInCartQuery : IRequest<IEnumerable<GamesListCartViewModel>>
    {
        public IEnumerable<Guid> GameIds { get; set; }
    }
}
