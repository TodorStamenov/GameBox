using GameBox.Application.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GameBox.Api.Controllers
{
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
                return this.mediator ?? (this.mediator = HttpContext.RequestServices.GetService<IMediator>());
            }
        }

        protected Guid UserId
        {
            get
            {
                return new Guid(User.Claims.FirstOrDefault(c => c.Type == Constants.Common.UserIdClaimKey)?.Value);
            }
        }
    }
}