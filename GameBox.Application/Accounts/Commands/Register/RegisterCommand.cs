using MediatR;

namespace GameBox.Application.Accounts.Commands.Register
{
    public class RegisterCommand : IRequest<RegisterViewModel>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }
    }
}
