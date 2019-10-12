using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameBox.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired();
            builder.HasIndex(r => r.Name).IsUnique();
            builder.Property(r => r.Name).HasMaxLength(Constants.Role.NameMaxLength);
        }
    }
}