using GameBox.Application.Games.Queries.GetGamesInCart;
using Microsoft.AspNetCore.Mvc;

namespace GameBox.Api.Controllers;

public class CartController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<IEnumerable<GamesListCartViewModel>>> Get([FromBody] IEnumerable<Guid> gameIds)
    {
        return base.Ok(await base.Mediator.Send(new GetGamesInCartQuery { GameIds = gameIds }));
    }
}
