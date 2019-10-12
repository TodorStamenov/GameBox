using GameBox.Application.Games.Commands.CreateGame;
using GameBox.Application.Games.Commands.DeleteGame;
using GameBox.Application.Games.Commands.UpdateGame;
using GameBox.Application.Games.Queries.GetAllGames;
using GameBox.Application.Games.Queries.GetGame;
using GameBox.Application.Games.Queries.GetGameDetails;
using GameBox.Application.Games.Queries.GetGamesByCategory;
using GameBox.Application.Games.Queries.GetGamesByTitle;
using GameBox.Application.Games.Queries.GetOwnedGames;
using GameBox.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Api.Controllers
{
    public class GamesController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<GamesListViewModel>>> Get([FromQuery]int loadedGames)
        {
            return Ok(await Mediator.Send(new GetAllGamesQuery { LoadedGames = loadedGames }));
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<GamesListViewModel>>> ByCategory([FromRoute]Guid categoryId, [FromQuery]int loadedGames)
        {
            var query = new GetGamesByCategoryQuery
            {
                CategoryId = categoryId,
                LoadedGames = loadedGames
            };

            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> Details([FromRoute]Guid id)
        {
            return Ok(await Mediator.Send(new GetGameDetailsQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        [Route("owned")]
        public async Task<ActionResult<IEnumerable<GamesListViewModel>>> Owned([FromQuery]int loadedGames)
        {
            var query = new GetOwnedGamesQuery
            {
                Username = User.Identity.Name,
                LoadedGames = loadedGames
            };
            
            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        [Authorize(Roles = Constants.Common.Admin)]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<GamesListByTitleViewModel>>> Get([FromQuery]string title)
        {
            return Ok(await Mediator.Send(new GetGamesByTitleQuery { Title = title }));
        }

        [HttpGet]
        [Authorize(Roles = Constants.Common.Admin)]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            return Ok(await Mediator.Send(new GetGameQuery { Id = id }));
        }

        [HttpPut]
        [Authorize(Roles = Constants.Common.Admin)]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute]Guid id, [FromBody]UpdateGameCommand command)
        {
            command.Id = id;

            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [Authorize(Roles = Constants.Common.Admin)]
        public async Task<IActionResult> Post([FromBody]CreateGameCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        [Authorize(Roles = Constants.Common.Admin)]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteGameCommand { Id = id }));
        }
    }
}