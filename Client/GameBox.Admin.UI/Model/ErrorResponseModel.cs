using System.Text.Json.Serialization;

namespace GameBox.Admin.UI.Model
{
    public class ErrorResponseModel
    {
        [JsonPropertyName("error")]
        public string[] Error { get; set; }
    }
}