using System;

namespace GameBox.Services.Models.View.Game
{
    public class ListGamesCartViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoId { get; set; }

        public string ThumbnailUrl { get; set; }

        public decimal Price { get; set; }
    }
}