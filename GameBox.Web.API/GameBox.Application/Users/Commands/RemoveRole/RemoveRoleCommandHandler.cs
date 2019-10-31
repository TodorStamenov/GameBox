using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Users.Commands.RemoveRole
{
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
                .UserRoles
                .Where(ur => ur.Role.Name == request.RoleName)
                .Where(ur => ur.User.Username == request.Username)
                .FirstOrDefaultAsync(cancellationToken);

            if (userRole == null)
            {
                throw new RoleEditException(request.Username, request.RoleName, false);
            }

            this.context.UserRoles.Remove(userRole);
            await this.context.SaveChangesAsync(cancellationToken);

            return $"User {request.RoleName} successfully removed from {request.Username} role.";
        }
    }
}
