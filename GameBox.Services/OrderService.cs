using GameBox.Core;
using GameBox.Core.Enums;
using GameBox.Data;
using GameBox.Data.Models;
using GameBox.Services.Contracts;
using GameBox.Services.Models.View.Orders;
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

        public ServiceResult Create(string username, IEnumerable<Guid> gameIds)
        {
            Guid userId = Database
                .Users
                .Where(u => u.Username == username)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == default(Guid))
            {
                return GetServiceResult(
                    string.Format(Constants.Common.NotExistingEntry, nameof(User), username),
                    ServiceResultType.Fail);
            }

            List<Game> games = Database
                .Games
                .Where(g => gameIds.Contains(g.Id))
                .ToList();

            if (!games.Any())
            {
                return GetServiceResult("Not valid game ids!", ServiceResultType.Fail);
            }

            foreach (var game in games)
            {
                game.OrderCount++;
            }

            Order order = new Order
            {
                UserId = userId,
                TimeStamp = DateTime.Now,
                Price = games.Sum(g => g.Price),
                Games = games
                    .Select(g => new GameOrder
                    {
                        GameId = g.Id
                    })
                    .ToList(),
            };

            Database.Orders.Add(order);
            Database.SaveChanges();

            return GetServiceResult(
                string.Format(Constants.Common.Success, nameof(Order), string.Empty, Constants.Common.Added),
                ServiceResultType.Success);
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