using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameBox.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.Price).HasColumnType("decimal(18, 2)");
        }
    }
}