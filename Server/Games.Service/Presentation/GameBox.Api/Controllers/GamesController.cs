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

namespace GameBox.Api.Controllers;

public class GamesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GamesListViewModel>>> Get([FromQuery] int loadedGames)
    {
        return base.Ok(await base.Mediator.Send(new GetAllGamesQuery { LoadedGames = loadedGames }));
    }

    [HttpGet("category/{categoryId:guid}")]
    public async Task<ActionResult<IEnumerable<GamesListViewModel>>> ByCategory([FromRoute] Guid categoryId, [FromQuery] int loadedGames)
    {
        var query = new GetGamesByCategoryQuery
        {
            CategoryId = categoryId,
            LoadedGames = loadedGames
        };

        return base.Ok(await base.Mediator.Send(query));
    }

    [HttpGet("details/{id:guid}")]
    public async Task<IActionResult> Details([FromRoute] Guid id)
    {
        return base.Ok(await base.Mediator.Send(new GetGameDetailsQuery { Id = id }));
    }

    [Authorize]
    [HttpGet("owned")]
    public async Task<ActionResult<IEnumerable<GamesListViewModel>>> Owned([FromQuery] int loadedGames)
    {
        var query = new GetOwnedGamesQuery
        {
            LoadedGames = loadedGames
        };

        return base.Ok(await base.Mediator.Send(query));
    }

    [HttpGet("all")]
    [Authorize(Roles = Constants.Common.Admin)]
    public async Task<ActionResult<IEnumerable<GamesListByTitleViewModel>>> Get([FromQuery] string title)
    {
        return base.Ok(await base.Mediator.Send(new GetGamesByTitleQuery { Title = title }));
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = Constants.Common.Admin)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        return base.Ok(await base.Mediator.Send(new GetGameQuery { Id = id }));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = Constants.Common.Admin)]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateGameCommand command)
    {
        command.Id = id;

        return base.Ok(await base.Mediator.Send(command));
    }

    [HttpPost]
    [Authorize(Roles = Constants.Common.Admin)]
    public async Task<IActionResult> Post([FromBody] CreateGameCommand command)
    {
        return base.Ok(await base.Mediator.Send(command));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Constants.Common.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        return base.Ok(await base.Mediator.Send(new DeleteGameCommand { Id = id }));
    }
}
