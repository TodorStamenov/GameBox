﻿using GameBox.Application.Model;
using System;
using System.Text.Json.Serialization;

namespace GameBox.Application.Orders.Commands.CreateOrder
{
    public class OrderCreatedMessage : QueueMessageModel
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("gamesCount")]
        public int GamesCount { get; set; }

        [JsonPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }
    }
}
