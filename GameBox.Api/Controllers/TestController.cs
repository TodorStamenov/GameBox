using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameBox.Api.Controllers
{
    [Authorize]
    public class TestController : BaseApiController
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "value1", "value2" };
        }
    }
}