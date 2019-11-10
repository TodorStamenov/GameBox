using MediatR;
using System;
using System.Collections.Generic;

namespace GameBox.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<CreateOrderViewModel>
    {
        public string Username { get; set; }

        public IEnumerable<Guid> GameIds { get; set; }
    }
}
