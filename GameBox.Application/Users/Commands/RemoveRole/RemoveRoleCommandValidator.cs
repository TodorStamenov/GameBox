using FluentValidation;

namespace GameBox.Application.Users.Commands.RemoveRole
{
    public class RemoveRoleCommandValidator : AbstractValidator<RemoveRoleCommand>
    {
        public RemoveRoleCommandValidator()
        {
            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.RoleName).NotEmpty();
        }
    }
}
