using GameBox.Application.Model;
using Newtonsoft.Json;

namespace GameBox.Application.Games.Commands.CreateGame
{
    public class GameCreatedMessage : QueueMessageModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}