using System.Text.Json.Serialization;

namespace Notification.Service.Messages;

public class GameCreatedMessage
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
}
