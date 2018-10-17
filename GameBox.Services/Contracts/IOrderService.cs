using GameBox.Services.Models.View.Order;
using System.Collections.Generic;

namespace GameBox.Services.Contracts
{
    public interface IOrderService
    {
        IEnumerable<OrderViewModel> All(string startDateString, string endDateString);
    }
}