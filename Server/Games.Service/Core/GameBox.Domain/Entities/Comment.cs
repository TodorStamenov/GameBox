using System;

namespace GameBox.Domain.Entities
{
    public class Comment : Entity<Guid>
    {
        public string Content { get; set; }

        public Guid GameId { get; set; }

        public Game Game { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public DateTime DateAdded { get; set; }
    }
}