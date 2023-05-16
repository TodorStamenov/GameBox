using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<Models.User>
{
    public void Configure(EntityTypeBuilder<Models.User> builder)
    {
        builder.Property(u => u.Username).IsRequired();
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.Username).HasMaxLength(50);
        builder.Property(u => u.Password).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(50);
        builder.Property(u => u.Salt).IsRequired();
    }
}
