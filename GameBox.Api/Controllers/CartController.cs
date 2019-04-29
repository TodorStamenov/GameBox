using GameBox.Services.Contracts;
using GameBox.Services.Models.View.Games;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GameBox.Api.Controllers
{
    public class CartController : BaseApiController
    {
        private readonly IGameService game;

        public CartController(IGameService game)
        {
            this.game = game;
        }

        [HttpPost]
        [Route("")]
        public IEnumerable<ListGamesCartViewModel> Get([FromBody]IEnumerable<Guid> gameIds)
        {
            return this.game.Cart(gameIds);
        }
    }
}