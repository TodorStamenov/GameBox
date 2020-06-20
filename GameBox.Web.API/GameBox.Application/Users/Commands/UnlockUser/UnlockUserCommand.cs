using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            private readonly IGameBoxDbContext context;

            public UnlockUserCommandHandler(IGameBoxDbContext context)
            {
                this.context = context;
            }

            public async Task<string> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
            {
                var user = await this.context
                    .Set<User>()
                    .Where(u => u.Username == request.Username)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException(nameof(User), request.Username);
                }

                if (!user.IsLocked)
                {
                    throw new AccountUnlockedException(request.Username);
                }

                user.IsLocked = false;

                await this.context.SaveChangesAsync(cancellationToken);

                return string.Format(Constants.Common.Success, nameof(User), request.Username, Constants.Common.Unlocked);
            }
        }
    }
}
