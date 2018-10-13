using Microsoft.AspNetCore.Mvc;

namespace GameBox.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseApiController : ControllerBase
    {
    }
}