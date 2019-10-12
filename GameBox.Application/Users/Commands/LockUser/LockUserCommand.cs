using MediatR;

namespace GameBox.Application.Users.Commands.LockUser
{
    public class LockUserCommand : IRequest<string>
    {
        public string Username { get; set; }
    }
}
