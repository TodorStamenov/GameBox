using System.Text.Json.Serialization;

namespace GameBox.Admin.UI.Model
{
    public class SuccessResponseModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}