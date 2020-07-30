using System.Text.Json.Serialization;

namespace GameBox.Admin.UI.Model
{
    public class CategoryListModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("games")]
        public int GamesCount { get; set; }
    }
}