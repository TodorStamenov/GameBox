using System.Text.Json.Serialization;

namespace GameBox.Admin.UI.Model
{
    public class CategoryMenuModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}