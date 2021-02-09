using System;

namespace GameBox.Domain.Entities
{
    public class Comment : Entity<Guid>
    {
        public string Content { get; set; }

        public Guid GameId { get; set; }

        public Game Game { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime DateAdded { get; set; }
    }
}