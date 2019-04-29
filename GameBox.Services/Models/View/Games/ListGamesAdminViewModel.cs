using System;

namespace GameBox.Services.Models.View.Games
{
    public class ListGamesAdminViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int ViewCount { get; set; }

        public decimal Price { get; set; }

        public int OrderCount { get; set; }
    }
}