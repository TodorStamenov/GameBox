using GameBox.Application.Accounts.Commands.Register;
using GameBox.Application.Infrastructure;
using GameBox.Application.Users.Commands.AddRole;
using GameBox.Application.Users.Commands.LockUser;
using GameBox.Application.Users.Commands.RemoveRole;
using GameBox.Application.Users.Commands.UnlockUser;
using GameBox.Application.Users.Querues.GetAllUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Api.Controllers
{
    [Authorize(Roles = Constants.Common.Admin)]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<UsersListViewModel>>> All([FromQuery]string username)
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery { QueryString = username }));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]RegisterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Route("lock")]
        public async Task<IActionResult> Lock([FromQuery]string username)
        {
            return Ok(await Mediator.Send(new LockUserCommand { Username = username }));
        }

        [HttpGet]
        [Route("unlock")]
        public async Task<IActionResult> Unlock([FromQuery]string username)
        {
            return Ok(await Mediator.Send(new UnlockUserCommand { Username = username }));
        }

        [HttpGet]
        [Route("addRole")]
        public async Task<IActionResult> AddRole([FromQuery]string username, [FromQuery]string roleName)
        {
            var command = new AddRoleCommand
            {
                Username = username,
                RoleName = roleName
            };

            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Route("removeRole")]
        public async Task<IActionResult> RemoveRole([FromQuery]string username, [FromQuery]string roleName)
        {
            var command = new RemoveRoleCommand
            {
                Username = username,
                RoleName = roleName
            };

            return Ok(await Mediator.Send(command));
        }
    }
}