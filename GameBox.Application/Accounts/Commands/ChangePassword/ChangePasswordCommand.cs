using MediatR;

namespace GameBox.Application.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<string>
    {
        public string Username { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string RepeatPassword { get; set; }
    }
}
