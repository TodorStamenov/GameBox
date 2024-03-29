﻿namespace GameBox.Domain.Entities;

public class Order : Entity<Guid>
{
    public Order()
    {
        this.Games = new HashSet<GameOrder>();
    }

    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; }

    public decimal Price { get; set; }

    public DateTime DateAdded { get; set; }

    public ICollection<GameOrder> Games { get; private set; }
}
