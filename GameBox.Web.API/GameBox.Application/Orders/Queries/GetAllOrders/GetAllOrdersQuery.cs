using MediatR;
using System.Collections.Generic;

namespace GameBox.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrdersListViewModel>>
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
