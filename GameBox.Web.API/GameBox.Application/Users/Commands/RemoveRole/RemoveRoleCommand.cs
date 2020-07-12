using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Users.Commands.RemoveRole
{
    public class RemoveRoleCommand : IRequest<string>
    {
        public string Username { get; set; }

        public string RoleName { get; set; }

        public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand, string>
        {
            private readonly IDataService context;

            public RemoveRoleCommandHandler(IDataService context)
            {
                this.context = context;
            }

            public async Task<string> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
            {
                var userRole = this.context
                    .All<UserRoles>()
                    .Where(ur => ur.Role.Name == request.RoleName)
                    .Where(ur => ur.User.Username == request.Username)
                    .FirstOrDefault();

                if (userRole == null)
                {
                    throw new RoleEditException(request.Username, request.RoleName, false);
                }

                await this.context.DeleteAsync(userRole);
                await this.context.SaveAsync(cancellationToken);

                return $"User {request.RoleName} successfully removed from {request.Username} role.";
            }
        }
    }
}
