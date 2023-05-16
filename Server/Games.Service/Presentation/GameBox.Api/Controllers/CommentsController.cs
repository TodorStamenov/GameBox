using GameBox.Application.Comments.Commands.CreateComment;
using GameBox.Application.Comments.Commands.DeleteComment;
using GameBox.Application.Comments.Queries.GetCommentsByGame;
using GameBox.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Api.Controllers;

public class CommentsController : BaseApiController
{
    [HttpGet("{gameId:guid}")]
    public async Task<ActionResult<IEnumerable<CommentsListViewModel>>> GetComments([FromRoute] Guid gameId)
    {
        return Ok(await Mediator.Send(new GetCommentsByGameQuery { GameId = gameId }));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] CreateCommentCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Constants.Common.Admin)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return Ok(await Mediator.Send(new DeleteCommentCommand { Id = id }));
    }
}
