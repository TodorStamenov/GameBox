using FluentValidation;
using GameBox.Application.Infrastructure;

namespace GameBox.Application.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(r => r.OldPassword)
                .Length(Constants.User.UsernameMinLength, Constants.User.UsernameMaxLength)
                .NotEmpty();

            RuleFor(r => r.NewPassword)
                .Length(Constants.User.PasswordMinLength, Constants.User.PasswordMaxLength)
                .NotEmpty();

            RuleFor(r => r.RepeatPassword)
                .Equal(r => r.RepeatPassword)
                .NotEmpty();
        }
    }
}
