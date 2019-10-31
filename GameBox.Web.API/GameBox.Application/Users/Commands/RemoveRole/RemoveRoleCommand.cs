using MediatR;

namespace GameBox.Application.Users.Commands.RemoveRole
{
    public class RemoveRoleCommand : IRequest<string>
    {
        public string Username { get; set; }

        public string RoleName { get; set; }
    }
}
