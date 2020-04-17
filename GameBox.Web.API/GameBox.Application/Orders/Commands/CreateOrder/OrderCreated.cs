using GameBox.Application.Contracts.Services;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Orders.Commands.CreateOrder
{
    public class OrderCreated : INotification
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("gamesCount")]
        public int GamesCount { get; set; }

        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; set; }

        public class OrderCreatedHandler : INotificationHandler<OrderCreated>
        {
            private readonly IMessageQueueSenderService sender;

            public OrderCreatedHandler(IMessageQueueSenderService sender)
            {
                this.sender = sender;
            }

            public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
            {
                return Task.Run(() => this.sender.Send(queueName: "orders", notification));
            }
        }
    }
}
