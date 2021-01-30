using FluentValidation;
using GameBox.Application.Infrastructure;

namespace GameBox.Application.Accounts.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r => r.Username)
                .Length(Constants.User.UsernameMinLength, Constants.User.UsernameMaxLength)
                .NotEmpty();

            RuleFor(r => r.Password)
                .Length(Constants.User.PasswordMinLength, Constants.User.PasswordMaxLength)
                .NotEmpty();

            RuleFor(r => r.RepeatPassword)
                .Equal(r => r.Password)
                .NotEmpty();
        }
    }
}
