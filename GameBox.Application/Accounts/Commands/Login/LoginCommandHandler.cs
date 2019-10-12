using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginViewModel>
    {
        private readonly IGameBoxDbContext context;
        private readonly IAccountService accountService;

        public LoginCommandHandler(IGameBoxDbContext context, IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
        }

        public async Task<LoginViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userInfo = await this.context
                .Users
                .Where(u => u.Username == request.Username)
                .Select(u => new
                {
                    u.Username,
                    u.Password,
                    u.Salt,
                    u.IsLocked,
                    IsAdmin = u.Roles
                        .Any(r => r.Role.Name == Constants.Common.Admin)
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (userInfo == null)
            {
                throw new InvalidCredentialsException();
            }

            if (userInfo.IsLocked)
            {
                throw new AccountLockedException(request.Username);
            }

            var hashedPassword = this.accountService.HashPassword(request.Password, userInfo.Salt);

            if (userInfo.Password != hashedPassword)
            {
                throw new InvalidCredentialsException();
            }

            var token = this.accountService.GenerateJwtToken(request.Username, userInfo.IsAdmin);

            return new LoginViewModel
            {
                Username = request.Username,
                Token = token,
                IsAdmin = userInfo.IsAdmin,
                Message = string.Format(Constants.Common.Success, nameof(User), request.Username, "Logged In")
            };
        }
    }
}
