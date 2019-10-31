using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;
using System;

namespace GameBox.Application.Games.Queries.GetAllGames
{
    public class GamesListViewModel : IMapFrom<Game>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoId { get; set; }

        public string ThumbnailUrl { get; set; }

        public decimal Price { get; set; }

        public double Size { get; set; }

        public int ViewCount { get; set; }
    }
}
