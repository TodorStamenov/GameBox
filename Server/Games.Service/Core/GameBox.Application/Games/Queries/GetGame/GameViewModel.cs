using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;
using System;

namespace GameBox.Application.Games.Queries.GetGame
{
    public class GameViewModel : IMapFrom<Game>
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public double Size { get; set; }

        public string VideoId { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Description { get; set; }

        public string ReleaseDate { get; set; }

        public Guid CategoryId { get; set; }
    }
}
