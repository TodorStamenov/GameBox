using GameBox.Application.Accounts.Commands.ChangePassword;
using GameBox.Application.Accounts.Commands.Login;
using GameBox.Application.Accounts.Commands.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameBox.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            command.Username = User.Identity.Name;

            return Ok(await Mediator.Send(command));
        }
    }
}