namespace GameBox.Domain.Entities;

public class Wishlist
{
    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; }

    public Guid GameId { get; set; }

    public Game Game { get; set; }
}
