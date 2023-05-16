using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameBox.Persistence;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Game> Games { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Wishlist> Wishlists { get; set; }

    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        base.OnModelCreating(builder);
    }
}
