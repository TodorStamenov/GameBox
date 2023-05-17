using GameBox.Application.Comments.Commands.CreateComment;
using GameBox.Application.Comments.Commands.DeleteComment;
using GameBox.Application.Comments.Queries.GetCommentsByGame;
using GameBox.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameBox.Api.Controllers;

public class CommentsController : BaseApiController
{
    [HttpGet("{gameId:guid}")]
    public async Task<ActionResult<IEnumerable<CommentsListViewModel>>> GetComments([FromRoute] Guid gameId)
    {
        return base.Ok(await base.Mediator.Send(new GetCommentsByGameQuery { GameId = gameId }));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] CreateCommentCommand command)
    {
        return base.Ok(await base.Mediator.Send(command));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Constants.Common.Admin)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return base.Ok(await base.Mediator.Send(new DeleteCommentCommand { Id = id }));
    }
}
