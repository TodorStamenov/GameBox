using Microsoft.AspNetCore.Mvc;

namespace User.Api.Controllers
{
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new { test = "Test123" });
        }
    }
}
