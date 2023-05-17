using System.Text.Json.Serialization;

namespace GameBox.Application.Orders.Commands.CreateOrder;

public class OrderGame
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("viewCount")]
    public int ViewCount { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("orderCount")]
    public int OrderCount { get; set; }
}
