using System;

namespace GameBox.Domain.Entities
{
    public class Wishlist
    {
        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid GameId { get; set; }

        public Game Game { get; set; }
    }
}
