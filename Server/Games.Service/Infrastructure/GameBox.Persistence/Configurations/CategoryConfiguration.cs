using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameBox.Persistence.Configurations;

public class CategoryConfiguration : BaseConfiguration<Guid, Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.HasIndex(c => c.Name).IsUnique();
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Name).HasMaxLength(Constants.Category.NameMaxLength);

        builder
            .HasMany(c => c.Games)
            .WithOne(g => g.Category)
            .HasForeignKey(g => g.CategoryId);
    }
}
