using GameBox.Application.Comments.Commands.CreateComment;
using GameBox.Application.Comments.Commands.DeleteComment;
using GameBox.Application.Comments.Commands.UpdateComment;
using GameBox.Application.Comments.Queries.GetComment;
using GameBox.Application.Comments.Queries.GetCommentsByGame;
using GameBox.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Api.Controllers
{
    [Authorize]
    public class CommentsController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CommentsListViewModel>>> GetComments(Guid gameId)
        {
            return Ok(await Mediator.Send(new GetCommentsByGameQuery { GameId = gameId }));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var command = new GetCommentQuery 
            { 
                Id = id,
                UserId = UserId,
                IsAdmin = User.IsInRole(Constants.Common.Admin)
            };

            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute]Guid id, [FromBody]UpdateCommentCommand command)
        {
            command.Id = id;
            command.UserId = UserId;
            command.IsAdmin = User.IsInRole(Constants.Common.Admin);

            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateCommentCommand command)
        {
            command.UserId = UserId;

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        [Authorize(Roles = Constants.Common.Admin)]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            return Ok(await Mediator.Send(new DeleteCommentCommand { Id = id }));
        }
    }
}