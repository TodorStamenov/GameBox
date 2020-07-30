using GameBox.Admin.UI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderListModel>> GetOrders(string startDate, string endDate);
    }
}