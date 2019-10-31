using FluentValidation;

namespace GameBox.Application.Users.Commands.AddRole
{
    public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.RoleName).NotEmpty();
        }
    }
}
