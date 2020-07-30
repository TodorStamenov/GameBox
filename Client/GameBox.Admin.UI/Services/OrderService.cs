using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services
{
    public class OrderService : IOrderService
    {
        private readonly string ordersUrl;
        private readonly IHttpClientService httpClient;

        public OrderService(
            IHttpClientService httpClient,
            ConfigurationSettings config)
        {
            this.httpClient = httpClient;
            this.ordersUrl = $"{config.OrdersApiUrl}orders/";
        }

        public async Task<IEnumerable<OrderListModel>> GetOrders(string startDate, string endDate)
        {
            return await this.httpClient.GetAsync<IEnumerable<OrderListModel>>($"{this.ordersUrl}?startDate={startDate}&endDate={endDate}");
        }
    }
}