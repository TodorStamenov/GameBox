using GameBox.Application.Model;
using System.Text.Json.Serialization;

namespace GameBox.Application.Games.Commands.CreateGame;

public class GameCreatedMessage : QueueMessageModel
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
}
