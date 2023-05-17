using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;

namespace GameBox.Application.Games.Queries.GetGamesInCart;

public class GamesListCartViewModel : IMapFrom<Game>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string VideoId { get; set; }

    public string ThumbnailUrl { get; set; }

    public decimal Price { get; set; }
}
