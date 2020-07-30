using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Pages.Orders
{
    [Route("/orders/all")]
    public partial class Orders : ComponentBase
    {
        public IEnumerable<OrderListModel> orders = new List<OrderListModel>();

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Inject] public IOrderService OrderService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.GetOrders();
        }

        public async Task OnFilterOrders()
        {
            await this.GetOrders(this.StartDate?.ToShortDateString(), this.EndDate?.ToShortDateString());
        }

        private async Task GetOrders(string startDate = "", string endDate = "")
        {
            this.orders = await this.OrderService.GetOrders(startDate, endDate);
        }
    }
}