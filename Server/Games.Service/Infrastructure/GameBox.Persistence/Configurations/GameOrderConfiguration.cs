using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameBox.Persistence.Configurations;

public class GameOrderConfiguration : IEntityTypeConfiguration<GameOrder>
{
    public void Configure(EntityTypeBuilder<GameOrder> builder)
    {
        builder.HasKey(go => new { go.GameId, go.OrderId });

        builder
            .HasOne(go => go.Game)
            .WithMany(g => g.Orders)
            .HasForeignKey(go => go.GameId);

        builder
            .HasOne(go => go.Order)
            .WithMany(o => o.Games)
            .HasForeignKey(go => go.OrderId);
    }
}
