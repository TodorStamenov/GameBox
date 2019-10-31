using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Users.Commands.AddRole
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, string>
    {
        private readonly IGameBoxDbContext context;

        public AddRoleCommandHandler(IGameBoxDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var userRoleInfo = await this.context
                .UserRoles
                .Where(ur => ur.Role.Name == request.RoleName)
                .Where(ur => ur.User.Username == request.Username)
                .Select(ur => new
                {
                    ur.RoleId,
                    ur.UserId
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (userRoleInfo != null)
            {
                throw new RoleEditException(request.Username, request.RoleName, true);
            }

            var userId = await this.context
                .Users
                .Where(u => u.Username == request.Username)
                .Select(u => u.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (userId == default(Guid))
            {
                throw new NotFoundException(nameof(User), request.Username);
            }

            var roleId = await this.context
                .Roles
                .Where(r => r.Name == request.RoleName)
                .Select(r => r.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (roleId == default(Guid))
            {
                throw new NotFoundException(nameof(Role), request.RoleName);
            }

            var userRole = new UserRoles
            {
                RoleId = roleId,
                UserId = userId
            };

            await this.context.UserRoles.AddAsync(userRole);
            await this.context.SaveChangesAsync(cancellationToken);

            return $"User {request.Username} successfully added to {request.RoleName} role.";
        }
    }
}
