using GameBox.Application.Games.Queries.GetGamesInCart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Api.Controllers
{
    public class CartController : BaseApiController
    {
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<IEnumerable<GamesListCartViewModel>>> Get([FromBody]IEnumerable<Guid> gameIds)
        {
            return Ok(await Mediator.Send(new GetGamesInCartQuery { GameIds = gameIds }));
        }
    }
}