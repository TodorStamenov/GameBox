using MediatR;

namespace GameBox.Application.Users.Commands.AddRole
{
    public class AddRoleCommand : IRequest<string>
    {
        public string Username { get; set; }

        public string RoleName { get; set; }
    }
}
