using GameBox.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameBox.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder
                .HasIndex(g => g.Title)
                .IsUnique();

            builder
                .Property(g => g.Price)
                .HasColumnType("decimal(18, 2)");

            builder
                .Property(g => g.Size)
                .HasColumnType("decimal(18, 2)");

            builder
                .Property(g => g.ReleaseDate)
                .HasColumnType("date");
        }
    }
}