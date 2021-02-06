using GameBox.Application.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Api.Controllers
{
    public class OrdersController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]IEnumerable<Guid> gameIds)
        {
            var command = new CreateOrderCommand
            {
                UserId = UserId,
                Username = User.Identity.Name,
                GameIds = gameIds
            };

            return Ok(await Mediator.Send(command));
        }
    }
}