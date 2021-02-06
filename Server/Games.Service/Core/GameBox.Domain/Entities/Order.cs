using System;
using System.Collections.Generic;

namespace GameBox.Domain.Entities
{
    public class Order : Entity<Guid>
    {
        public Order()
        {
            this.Games = new HashSet<GameOrder>();
        }

        public Guid UserId { get; set; }

        public Customer User { get; set; }

        public decimal Price { get; set; }

        public DateTime DateAdded { get; set; }

        public ICollection<GameOrder> Games { get; private set; }
    }
}