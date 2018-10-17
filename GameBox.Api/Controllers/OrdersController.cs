using GameBox.Services.Contracts;
using GameBox.Services.Models.View.Order;
using Microsoft.AspNetCore.Mvc;
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
        [Route("")]
        public IEnumerable<OrderViewModel> Get([FromQuery]string startDate, [FromQuery]string endDate)
        {
            return this.order.All(startDate, endDate);
        }
    }
}