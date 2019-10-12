using FluentValidation;

namespace GameBox.Application.Users.Commands.LockUser
{
    public class LockUserCommandValidator : AbstractValidator<LockUserCommand>
    {
        public LockUserCommandValidator()
        {
            RuleFor(u => u.Username).NotEmpty();
        }
    }
}
