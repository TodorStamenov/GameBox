using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using User.Services.BindingModels.Auth;
using User.Services.Contracts;

namespace User.Api.Controllers;

public class AuthController : BaseApiController
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginBindingModel model)
    {
        return Ok(await this.authService.LoginAsync(model));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterBindingModel model)
    {
        return Ok(await this.authService.RegisterAsync(model));
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordBindingModel model)
    {
        model.Username = User.Identity.Name;

        return Ok(await this.authService.ChangePasswordAsync(model));
    }
}
