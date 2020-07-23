using System;
using System.Collections.Generic;

namespace GameBox.Domain.Entities
{
    public class Game : Entity<Guid>
    {
        public Game()
        {
            this.Orders = new HashSet<GameOrder>();
            this.Wishlists = new HashSet<Wishlist>();
            this.Comments = new HashSet<Comment>();
        }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public double Size { get; set; }

        public string VideoId { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int ViewCount { get; set; }

        public int OrderCount { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<GameOrder> Orders { get; private set; }

        public ICollection<Wishlist> Wishlists { get; private set; }

        public ICollection<Comment> Comments { get; private set; }
    }
}