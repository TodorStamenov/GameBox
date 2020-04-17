﻿using System;
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

        public User User { get; set; }

        public DateTime TimeStamp { get; set; }

        public decimal Price { get; set; }

        public ICollection<GameOrder> Games { get; private set; }
    }
}