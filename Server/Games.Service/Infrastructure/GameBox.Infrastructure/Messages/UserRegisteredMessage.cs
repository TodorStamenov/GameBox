namespace GameBox.Infrastructure.Messages;

public class UserRegisteredMessage
{
    public Guid UserId { get; set; }

    public string Username { get; set; }
}
