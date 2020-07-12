using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Users.Commands.LockUser
{
    public class LockUserCommand : IRequest<string>
    {
        public string Username { get; set; }

        public class LockUserCommandHandler : IRequestHandler<LockUserCommand, string>
        {
            private readonly IDataService context;

            public LockUserCommandHandler(IDataService context)
            {
                this.context = context;
            }

            public async Task<string> Handle(LockUserCommand request, CancellationToken cancellationToken)
            {
                var user = this.context
                    .All<User>()
                    .Where(u => u.Username == request.Username)
                    .FirstOrDefault();

                if (user == null)
                {
                    throw new NotFoundException(nameof(User), request.Username);
                }

                if (user.IsLocked)
                {
                    throw new AccountLockedException(request.Username);
                }

                user.IsLocked = true;

                await this.context.SaveAsync(cancellationToken);

                return string.Format(Constants.Common.Success, nameof(User), request.Username, Constants.Common.Locked);
            }
        }
    }
}
