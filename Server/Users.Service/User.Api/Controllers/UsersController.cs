using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Services.BindingModels.Auth;
using User.Services.Contracts;
using User.Services.Infrastructure;
using User.Services.ViewModels.Users;

namespace User.Api.Controllers;

[Authorize(Roles = Constants.Common.Admin)]
public class UsersController : BaseApiController
{
    private readonly IAuthService authService;
    private readonly IUserService userService;

    public UsersController(
        IAuthService authService,
        IUserService userService)
    {
        this.authService = authService;
        this.userService = userService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<UsersListViewModel>>> All([FromQuery] string username)
    {
        return Ok(await this.userService.GetAllUsersAsync(username));
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] RegisterBindingModel model)
    {
        return Ok(await this.authService.RegisterAsync(model));
    }

    [HttpGet("lock")]
    public async Task<IActionResult> Lock([FromQuery] string username)
    {
        return Ok(await this.userService.LockAsync(username));
    }

    [HttpGet("unlock")]
    public async Task<IActionResult> Unlock([FromQuery] string username)
    {
        return Ok(await this.userService.UnockAsync(username));
    }

    [HttpGet("add-role")]
    public async Task<IActionResult> AddRole([FromQuery] string username, [FromQuery] string roleName)
    {
        return Ok(await this.userService.AddRoleAsync(username, roleName));
    }

    [HttpGet("remove-role")]
    public async Task<IActionResult> RemoveRole([FromQuery] string username, [FromQuery] string roleName)
    {
        return Ok(await this.userService.RemoveRoleAsync(username, roleName));
    }
}
