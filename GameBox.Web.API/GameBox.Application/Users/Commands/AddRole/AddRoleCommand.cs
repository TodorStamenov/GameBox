using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Users.Commands.AddRole
{
    public class AddRoleCommand : IRequest<string>
    {
        public string Username { get; set; }

        public string RoleName { get; set; }

        public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, string>
        {
            private readonly IDataService context;

            public AddRoleCommandHandler(IDataService context)
            {
                this.context = context;
            }

            public async Task<string> Handle(AddRoleCommand request, CancellationToken cancellationToken)
            {
                var userRoleInfo = this.context
                    .All<UserRoles>()
                    .Where(ur => ur.Role.Name == request.RoleName)
                    .Where(ur => ur.User.Username == request.Username)
                    .Select(ur => new
                    {
                        ur.RoleId,
                        ur.UserId
                    })
                    .FirstOrDefault();

                if (userRoleInfo != null)
                {
                    throw new RoleEditException(request.Username, request.RoleName, true);
                }

                var userId = this.context
                    .All<User>()
                    .Where(u => u.Username == request.Username)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                if (userId == default(Guid))
                {
                    throw new NotFoundException(nameof(User), request.Username);
                }

                var roleId = this.context
                    .All<Role>()
                    .Where(r => r.Name == request.RoleName)
                    .Select(r => r.Id)
                    .FirstOrDefault();

                if (roleId == default(Guid))
                {
                    throw new NotFoundException(nameof(Role), request.RoleName);
                }

                var userRole = new UserRoles
                {
                    RoleId = roleId,
                    UserId = userId
                };

                await this.context.AddAsync(userRole);
                await this.context.SaveAsync(cancellationToken);

                return $"User {request.Username} successfully added to {request.RoleName} role.";
            }
        }
    }
}
