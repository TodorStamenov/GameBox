using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameBox.Persistence.Configurations;

public class CustomerConfiguration : BaseConfiguration<Guid, Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.Username).IsRequired();
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.Username).HasMaxLength(Constants.User.UsernameMaxLength);

        builder
            .HasMany(u => u.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId);

        builder
            .HasMany(u => u.Comments)
            .WithOne(c => c.Customer)
            .HasForeignKey(c => c.CustomerId);
    }
}
