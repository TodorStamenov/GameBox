using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Message.DataAccess.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Models.Message>
{
    public void Configure(EntityTypeBuilder<Models.Message> builder)
    {
        builder
            .Ignore(m => m.Data);

        builder
            .Property(nameof(Models.Message.serializedData))
            .IsRequired()
            .HasField(nameof(Models.Message.serializedData));

        builder
            .Property(m => m.QueueName)
            .IsRequired();

        builder
            .Property(m => m.Type)
            .IsRequired()
            .HasConversion(
                t => t.AssemblyQualifiedName,
                t => Type.GetType(t));
    }
}
