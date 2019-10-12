using MediatR;

namespace GameBox.Application.Accounts.Commands.Login
{
    public class LoginCommand : IRequest<LoginViewModel>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
