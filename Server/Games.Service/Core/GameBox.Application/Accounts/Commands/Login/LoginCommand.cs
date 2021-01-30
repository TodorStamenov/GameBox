using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Commands.Login
{
    public class LoginCommand : IRequest<LoginViewModel>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginViewModel>
        {
            private readonly IAccountService account;
            private readonly IDateTimeService dateTime;
            private readonly IDataService context;

            public LoginCommandHandler(IAccountService account, IDateTimeService dateTime, IDataService context)
            {
                this.account = account;
                this.dateTime = dateTime;
                this.context = context;
            }

            public async Task<LoginViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var userInfo = this.context
                    .All<User>()
                    .Where(u => u.Username == request.Username)
                    .Select(u => new
                    {
                        u.Id,
                        u.Username,
                        u.Password,
                        u.Salt,
                        u.IsLocked,
                        IsAdmin = u.Roles.Any(r => r.Role.Name == Constants.Common.Admin)
                    })
                    .FirstOrDefault();

                if (userInfo == null)
                {
                    throw new InvalidCredentialsException();
                }

                if (userInfo.IsLocked)
                {
                    throw new AccountLockedException(userInfo.Username);
                }

                var hashedPassword = this.account.HashPassword(request.Password, userInfo.Salt);

                if (userInfo.Password != hashedPassword)
                {
                    throw new InvalidCredentialsException();
                }

                var token = this.account.GenerateJwtToken(userInfo.Id.ToString(), userInfo.Username, userInfo.IsAdmin);

                return await Task.FromResult(new LoginViewModel
                {
                    Id = userInfo.Id,
                    Username = userInfo.Username,
                    IsAdmin = userInfo.IsAdmin,
                    Token = token,
                    ExpirationDate = this.dateTime.UtcNow.AddDays(Constants.Common.TokenExpiration),
                    Message = string.Format(Constants.Common.Success, nameof(User), userInfo.Username, "Logged In")
                });
            }
        }
    }
}
