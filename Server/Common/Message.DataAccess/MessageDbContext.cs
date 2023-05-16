using Microsoft.EntityFrameworkCore;

namespace Message.DataAccess;

public class MessageDbContext : DbContext
{
    public MessageDbContext(DbContextOptions<MessageDbContext> options)
        : base(options)
    {
    }

    public DbSet<Models.Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        base.OnModelCreating(builder);
    }
}
