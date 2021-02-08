using GameBox.Application.GraphQL.Queries.GraphQlQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameBox.Api.Controllers
{
    [Authorize]
    public class GraphQlController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<object>> Post([FromBody]GraphQlQuery query)
        {
            return this.Ok(await Mediator.Send(query));
        }
    }
}
