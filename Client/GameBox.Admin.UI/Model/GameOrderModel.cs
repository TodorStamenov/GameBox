using System.Text.Json.Serialization;

namespace GameBox.Admin.UI.Model
{
    public class GameOrderModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("viewCount")]
        public int ViewCount { get; set; }

        [JsonPropertyName("orderCount")]
        public int OrderCount { get; set; }
    }
}
