using GameBox.Data;
using GameBox.Services.Contracts;
using GameBox.Services.Models.View.Order;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameBox.Services
{
    public class OrderService : Service, IOrderService
    {
        public OrderService(GameBoxDbContext database)
            : base(database)
        {
        }

        public IEnumerable<OrderViewModel> All(string startDateString, string endDateString)
        {
            DateTime.TryParse(startDateString, out DateTime startDate);

            if (!DateTime.TryParse(endDateString, out DateTime endDate))
            {
                endDate = DateTime.MaxValue;
            }
            else
            {
                endDate = endDate.AddDays(1);
            }

            return Database
                .Orders
                .Where(o => startDate <= o.TimeStamp && o.TimeStamp <= endDate)
                .OrderByDescending(o => o.TimeStamp)
                .Take(50)
                .Select(o => new OrderViewModel
                {
                    Username = o.User.Username,
                    Price = o.Price,
                    TimeStamp = o.TimeStamp.ToShortDateString(),
                    GamesCount = o.Games.Count
                })
                .ToList();
        }
    }
}