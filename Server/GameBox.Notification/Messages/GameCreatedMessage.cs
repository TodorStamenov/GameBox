using System.Text.Json.Serialization;

namespace GameBox.Notification.Messages
{
    public class GameCreatedMessage
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}