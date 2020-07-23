using System;

namespace GameBox.Scheduler.Model
{
    public class Message
    {
        public Guid Id { get; set; }

        public string QueueName { get; set; }

        public string SerializedData { get; set; }
    }
}