using GameBox.Application.Accounts.Queries.GenerateJwtToken;
using GameBox.Application.Accounts.Queries.HashPassword;
using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginViewModel>
    {
        private readonly IMediator mediator;
        private readonly IGameBoxDbContext context;

        public LoginCommandHandler(IMediator mediator, IGameBoxDbContext context)
        {
            this.mediator = mediator;
            this.context = context;
        }

        public async Task<LoginViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userInfo = await this.context
                .Users
                .Where(u => u.Username == request.Username)
                .Select(u => new
                {
                    u.Id,
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
                throw new AccountLockedException(userInfo.Username);
            }

            var hashedPassword = await this.mediator.Send(new HashPasswordQuery
            {
                Password = request.Password,
                Salt = userInfo.Salt
            });

            if (userInfo.Password != hashedPassword)
            {
                throw new InvalidCredentialsException();
            }

            var token = await this.mediator.Send(new GenerateJwtTokenQuery
            {
                Username = userInfo.Username,
                IsAdmin = userInfo.IsAdmin
            });

            return new LoginViewModel
            {
                Username = userInfo.Username,
                Token = token,
                IsAdmin = userInfo.IsAdmin,
                ExpirationDate = DateTime.Now.AddDays(1),
                Message = string.Format(Constants.Common.Success, nameof(User), userInfo.Username, "Logged In")
            };
        }
    }
}
