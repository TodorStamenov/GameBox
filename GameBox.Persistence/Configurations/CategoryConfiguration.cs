using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameBox.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.HasIndex(c => c.Name).IsUnique();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(Constants.Category.NameMaxLength);

            builder
                .HasMany(c => c.Games)
                .WithOne(g => g.Category)
                .HasForeignKey(g => g.CategoryId);
        }
    }
}