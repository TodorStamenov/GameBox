using System;
using System.Text.Json;

namespace GameBox.Domain.Entities
{
    public class Message : Entity<Guid>
    {
        public string serializedData;

        public Message(string queueName, object data) {
            this.QueueName = queueName;
            this.Data = data;
        }
        
        private Message()
        { }

        public string QueueName { get; private set; }

        public Type Type { get; private set; }

        public bool Published { get; private set; }

        public void MarkAsPublished() 
        {
            this.Published = true;
        }

        public object Data
        {
            get 
            { 
                return JsonSerializer.Deserialize(
                    this.serializedData,
                    this.Type,
                    new JsonSerializerOptions { IgnoreNullValues = true });
            }
            
            set
            {
                this.Type = value.GetType();

                this.serializedData = JsonSerializer.Serialize(
                    value,
                    new JsonSerializerOptions { IgnoreNullValues = true });
            }
        }
    }
}