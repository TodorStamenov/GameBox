using System;

namespace GameBox.Domain.Entities
{
    public class Wishlist
    {
        public Guid UserId { get; set; }

        public Customer User { get; set; }

        public Guid GameId { get; set; }

        public Game Game { get; set; }
    }
}
