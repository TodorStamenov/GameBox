using GameBox.Application.Model;
using Newtonsoft.Json;
using System;

namespace GameBox.Application.Orders.Commands.CreateOrder
{
    public class OrderCreatedMessage : QueueMessageModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("gamesCount")]
        public int GamesCount { get; set; }

        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; set; }
    }
}
