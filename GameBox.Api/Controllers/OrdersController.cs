using GameBox.Application.Infrastructure;
using GameBox.Application.Orders.Commands.CreateOrder;
using GameBox.Application.Orders.Queries.GetAllOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Api.Controllers
{
    public class OrdersController : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = Constants.Common.Admin)]
        [Route("")]
        public async Task<ActionResult<IEnumerable<OrdersListViewModel>>> Get([FromQuery]string startDate, [FromQuery]string endDate)
        {
            var query = new GetAllOrdersQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> Post([FromBody]IEnumerable<Guid> gameIds)
        {
            var command = new CreateOrderCommand
            {
                Username = User.Identity.Name,
                GameIds = gameIds
            };

            return Ok(await Mediator.Send(command));
        }
    }
}