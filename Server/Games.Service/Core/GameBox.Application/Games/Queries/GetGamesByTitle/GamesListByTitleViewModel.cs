using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;

namespace GameBox.Application.Games.Queries.GetGamesByTitle;

public class GamesListByTitleViewModel : IMapFrom<Game>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public int ViewCount { get; set; }

    public decimal Price { get; set; }

    public int OrderCount { get; set; }
}
