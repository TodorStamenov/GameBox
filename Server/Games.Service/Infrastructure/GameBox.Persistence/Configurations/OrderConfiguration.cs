using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GameBox.Persistence.Configurations
{
    public class OrderConfiguration : BaseConfiguration<Guid, Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.Price).HasColumnType("decimal(18, 2)");
        }
    }
}