using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GameBox.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BaseApiController : ControllerBase
{
    private IMediator mediator;

    protected IMediator Mediator
    {
        get
        {
            return this.mediator ??= this.HttpContext.RequestServices.GetService<IMediator>();
        }
    }
}
