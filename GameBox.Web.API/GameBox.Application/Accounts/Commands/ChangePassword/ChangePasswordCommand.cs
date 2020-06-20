using GameBox.Application.Contracts;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ChangePasswordViewModel>
    {
        public string Username { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string RepeatPassword { get; set; }

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordViewModel>
        {
            private readonly IAccountService account;
            private readonly IGameBoxDbContext context;

            public ChangePasswordCommandHandler(IAccountService account, IGameBoxDbContext context)
            {
                this.account = account;
                this.context = context;
            }

            public async Task<ChangePasswordViewModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await this.context
                    .Set<User>()
                    .Where(u => u.Username == request.Username)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException(nameof(User), request.Username);
                }

                var oldHashedPassword = this.account.HashPassword(request.OldPassword, user.Salt);

                if (oldHashedPassword != user.Password)
                {
                    throw new InvalidCredentialsException();
                }

                user.Salt = this.account.GenerateSalt();

                var newHashedPassword = this.account.HashPassword(request.NewPassword, user.Salt);

                user.Password = newHashedPassword;

                await this.context.SaveChangesAsync(cancellationToken);

                return new ChangePasswordViewModel { Message = "You have successfully updated your password!" };
            }
        }
    }
}
