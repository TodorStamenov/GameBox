using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GameBox.Admin.UI.Model
{
    public class OrderListModel
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("gamesCount")]
        public int GamesCount { get; set; }

        [JsonPropertyName("games")]
        public IEnumerable<GameOrderModel> Games { get; set; }
    }
}