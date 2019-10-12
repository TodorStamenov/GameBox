using AutoMapper;
using GameBox.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrdersListViewModel>>
    {
        private const int OrdersOnPage = 50;

        private readonly IMapper mapper;
        private readonly IGameBoxDbContext context;

        public GetAllOrdersQueryHandler(IMapper mapper, IGameBoxDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<OrdersListViewModel>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            DateTime.TryParse(request.StartDate, out DateTime startDate);

            if (!DateTime.TryParse(request.EndDate, out DateTime endDate))
            {
                endDate = DateTime.MaxValue;
            }
            else
            {
                endDate = endDate.AddDays(1);
            }

            var orders = await this.context
                .Orders
                .Include(o => o.User)
                .Include(o => o.Games)
                .Where(o => startDate <= o.TimeStamp && o.TimeStamp <= endDate)
                .OrderByDescending(o => o.TimeStamp)
                .Take(OrdersOnPage)
                .ToListAsync(cancellationToken);

            return this.mapper.Map<IEnumerable<OrdersListViewModel>>(orders);
        }
    }
}
