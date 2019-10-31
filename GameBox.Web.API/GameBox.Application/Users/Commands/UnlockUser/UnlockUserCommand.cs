using MediatR;

namespace GameBox.Application.Users.Commands.UnlockUser
{
    public class UnlockUserCommand : IRequest<string>
    {
        public string Username { get; set; }
    }
}
