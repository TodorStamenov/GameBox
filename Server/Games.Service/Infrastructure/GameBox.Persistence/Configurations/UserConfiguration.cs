using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GameBox.Persistence.Configurations
{
    public class UserConfiguration : BaseConfiguration<Guid, User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.Username).IsRequired();
            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(u => u.Username).HasMaxLength(Constants.User.UsernameMaxLength);
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(Constants.User.PasswordMaxLength);

            builder
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            builder
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        }
    }
}