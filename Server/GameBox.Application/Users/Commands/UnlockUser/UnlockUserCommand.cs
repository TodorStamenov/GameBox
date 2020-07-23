using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Users.Commands.UnlockUser
{
    public class UnlockUserCommand : IRequest<string>
    {
        public string Username { get; set; }

        public class UnlockUserCommandHandler : IRequestHandler<UnlockUserCommand, string>
        {
            private readonly IDataService context;

            public UnlockUserCommandHandler(IDataService context)
            {
                this.context = context;
            }

            public async Task<string> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
            {
                var user = this.context
                    .All<User>()
                    .Where(u => u.Username == request.Username)
                    .FirstOrDefault();

                if (user == null)
                {
                    throw new NotFoundException(nameof(User), request.Username);
                }

                if (!user.IsLocked)
                {
                    throw new AccountUnlockedException(request.Username);
                }

                user.IsLocked = false;

                await this.context.SaveAsync(cancellationToken);

                return string.Format(Constants.Common.Success, nameof(User), request.Username, Constants.Common.Unlocked);
            }
        }
    }
}
