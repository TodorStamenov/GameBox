using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameBox.Persistence.Configurations;

public class GameConfiguration : BaseConfiguration<Guid, Game>
{
    public override void Configure(EntityTypeBuilder<Game> builder)
    {
        base.Configure(builder);

        builder.HasIndex(g => g.Title).IsUnique();
        builder.Property(g => g.Title).IsRequired();
        builder.Property(g => g.Title).HasMaxLength(Constants.Game.TitleMaxLength);
        builder.Property(g => g.Price).HasColumnType("decimal(18, 2)");
        builder.Property(g => g.Size).HasColumnType("decimal(18, 2)");
        builder.Property(g => g.VideoId).IsRequired();
        builder.Property(g => g.VideoId).HasMaxLength(Constants.Game.MaxVideoIdLength).IsFixedLength();
        builder.Property(g => g.Description).IsRequired();
        builder.Property(g => g.ReleaseDate).HasColumnType("date");

        builder
            .HasMany(g => g.Comments)
            .WithOne(c => c.Game)
            .HasForeignKey(c => c.GameId);
    }
}
