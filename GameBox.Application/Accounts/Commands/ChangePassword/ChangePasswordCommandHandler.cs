using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, string>
    {
        private readonly IGameBoxDbContext context;
        private readonly IAccountService accountService;

        public ChangePasswordCommandHandler(IGameBoxDbContext context, IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
        }

        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await this.context
                .Users
                .Where(u => u.Username == request.Username)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Username);
            }

            var oldHashedPassword = this.accountService.HashPassword(request.OldPassword, user.Salt);

            if (oldHashedPassword != user.Password)
            {
                throw new InvalidCredentialsException();
            }

            user.Salt = this.accountService.GenerateSalt();

            var newHashedPassword = this.accountService.HashPassword(request.NewPassword, user.Salt);

            user.Password = newHashedPassword;

            await this.context.SaveChangesAsync(cancellationToken);

            return "You have successfully updated your password!";
        }
    }
}
