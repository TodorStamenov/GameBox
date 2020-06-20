using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            private readonly IGameBoxDbContext context;

            public RemoveRoleCommandHandler(IGameBoxDbContext context)
            {
                this.context = context;
            }

            public async Task<string> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
            {
                var userRole = await this.context
                    .Set<UserRoles>()
                    .Where(ur => ur.Role.Name == request.RoleName)
                    .Where(ur => ur.User.Username == request.Username)
                    .FirstOrDefaultAsync(cancellationToken);

                if (userRole == null)
                {
                    throw new RoleEditException(request.Username, request.RoleName, false);
                }

                this.context.Set<UserRoles>().Remove(userRole);
                await this.context.SaveChangesAsync(cancellationToken);

                return $"User {request.RoleName} successfully removed from {request.Username} role.";
            }
        }
    }
}
