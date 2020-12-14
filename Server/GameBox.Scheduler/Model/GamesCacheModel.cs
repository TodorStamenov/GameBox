using System;

namespace GameBox.Scheduler.Model
{
    public class GamesCacheModel
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoId { get; set; }

        public string ThumbnailUrl { get; set; }

        public decimal Price { get; set; }

        public double Size { get; set; }

        public int ViewCount { get; set; }
    }
}