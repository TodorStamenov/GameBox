using FluentValidation;

namespace GameBox.Application.Accounts.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(r => r.Username).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();
        }
    }
}
