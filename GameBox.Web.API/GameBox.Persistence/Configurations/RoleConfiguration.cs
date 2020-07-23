using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GameBox.Persistence.Configurations
{
    public class RoleConfiguration : BaseConfiguration<Guid, Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.Name).IsRequired();
            builder.HasIndex(r => r.Name).IsUnique();
            builder.Property(r => r.Name).HasMaxLength(Constants.Role.NameMaxLength);
        }
    }
}