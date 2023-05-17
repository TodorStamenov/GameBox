using GameBox.Application.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameBox.Api.Controllers;

public class OrdersController : BaseApiController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] IEnumerable<Guid> gameIds)
    {
        var command = new CreateOrderCommand
        {
            Username = base.User.Identity.Name,
            GameIds = gameIds
        };

        return base.Ok(await base.Mediator.Send(command));
    }
}
