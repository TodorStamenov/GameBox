using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameBox.Persistence.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder
                .HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId);

            builder
                .HasOne(ur => ur.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}