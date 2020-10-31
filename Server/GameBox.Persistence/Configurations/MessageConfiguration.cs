using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GameBox.Persistence.Configurations
{
    public class MessageConfiguration : BaseConfiguration<Guid, Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            base.Configure(builder);

            builder
                .Ignore(m => m.Data);

            builder
                .Property(nameof(Message.serializedData))
                .IsRequired()
                .HasField(nameof(Message.serializedData));

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
}