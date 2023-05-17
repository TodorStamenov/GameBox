namespace GameBox.Domain.Entities;

public class Customer : Entity<Guid>
{
    public Customer()
    {
        this.Orders = new HashSet<Order>();
        this.Wishlist = new HashSet<Wishlist>();
        this.Comments = new HashSet<Comment>();
    }

    public Guid UserId { get; set; }

    public string Username { get; set; }

    public ICollection<Order> Orders { get; private set; }

    public ICollection<Wishlist> Wishlist { get; private set; }

    public ICollection<Comment> Comments { get; private set; }
}
