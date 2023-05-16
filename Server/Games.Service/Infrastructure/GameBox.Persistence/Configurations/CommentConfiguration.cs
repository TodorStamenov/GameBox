using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GameBox.Persistence.Configurations;

public class CommentConfiguration : BaseConfiguration<Guid, Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Content).IsRequired();
    }
}
