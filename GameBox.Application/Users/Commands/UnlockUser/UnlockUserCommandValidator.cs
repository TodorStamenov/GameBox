using FluentValidation;

namespace GameBox.Application.Users.Commands.UnlockUser
{
    public class UnlockUserCommandValidator : AbstractValidator<UnlockUserCommand>
    {
        public UnlockUserCommandValidator()
        {
            RuleFor(u => u.Username).NotEmpty();
        }
    }
}
