using GameBox.Core;
using GameBox.Core.Enums;
using GameBox.Services.Contracts;
using GameBox.Services.Models.View.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GameBox.Api.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService order;

        public OrdersController(IOrderService order)
        {
            this.order = order;
        }

        [HttpGet]
        [Authorize(Roles = Constants.Common.Admin)]
        [Route("")]
        public IEnumerable<OrderViewModel> Get([FromQuery]string startDate, [FromQuery]string endDate)
        {
            return this.order.All(startDate, endDate);
        }

        [HttpPost]
        [Authorize]
        [Route("")]
        public IActionResult Post([FromBody]IEnumerable<Guid> gameIds)
        {
            ServiceResult result = this.order.Create(User.Identity.Name, gameIds);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return Ok(result);
        }
    }
}