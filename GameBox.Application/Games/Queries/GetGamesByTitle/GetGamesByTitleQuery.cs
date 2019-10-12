using MediatR;
using System.Collections.Generic;

namespace GameBox.Application.Games.Queries.GetGamesByTitle
{
    public class GetGamesByTitleQuery : IRequest<IEnumerable<GamesListByTitleViewModel>>
    {
        public string Title { get; set; }
    }
}
