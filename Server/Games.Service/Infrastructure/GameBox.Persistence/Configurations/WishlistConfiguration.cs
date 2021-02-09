using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameBox.Persistence.Configurations
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(w => new { w.CustomerId, w.GameId });

            builder
                .HasOne(w => w.Customer)
                .WithMany(u => u.Wishlist)
                .HasForeignKey(w => w.CustomerId);

            builder
                .HasOne(w => w.Game)
                .WithMany(g => g.Wishlists)
                .HasForeignKey(w => w.GameId);
        }
    }
}
