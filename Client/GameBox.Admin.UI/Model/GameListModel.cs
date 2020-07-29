using System.Text.Json.Serialization;

namespace GameBox.Admin.UI.Model
{
    public class GamesListModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("viewCount")]
        public int ViewCount { get; set; }

        [JsonPropertyName("orderCount")]
        public int OrderCount { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}