using System;

namespace GameBox.Services.Models.View.Game
{
    public class ListGamesViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public double Size { get; set; }

        public string VideoId { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Description { get; set; }

        public int ViewCount { get; set; }
    }
}