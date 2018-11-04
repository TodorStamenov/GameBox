using GameBox.Core;
using GameBox.Services.Models.View.Orders;
using System;
using System.Collections.Generic;

namespace GameBox.Services.Contracts
{
    public interface IOrderService
    {
        ServiceResult Create(string username, IEnumerable<Guid> gameIds);

        IEnumerable<OrderViewModel> All(string startDateString, string endDateString);
    }
}