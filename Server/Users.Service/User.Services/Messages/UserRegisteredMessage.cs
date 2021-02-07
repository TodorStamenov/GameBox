using System;

namespace User.Services.Messages
{
    public class UserRegisteredMessage : QueueMessageModel
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }
    }
}
