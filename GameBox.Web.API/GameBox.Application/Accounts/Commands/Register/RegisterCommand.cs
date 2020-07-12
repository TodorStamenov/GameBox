using GameBox.Application.Contracts.Services;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Commands.Register
{
    public class RegisterCommand : IRequest<RegisterViewModel>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterViewModel>
        {
            private readonly IAccountService account;
            private readonly IDateTimeService dateTime;
            private readonly IDataService context;

            public RegisterCommandHandler(IAccountService account, IDateTimeService dateTime, IDataService context)
            {
                this.account = account;
                this.dateTime = dateTime;
                this.context = context;
            }

            public async Task<RegisterViewModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var salt = this.account.GenerateSalt();
                var hashedPassword = this.account.HashPassword(request.Password, salt);

                var user = new User
                {
                    Username = request.Username,
                    Password = hashedPassword,
                    Salt = salt
                };

                await this.context.AddAsync(user);
                await this.context.SaveAsync(cancellationToken);

                var token = this.account.GenerateJwtToken(user.Id.ToString(), user.Username, false);

                return new RegisterViewModel
                {
                    Username = user.Username,
                    Token = token,
                    IsAdmin = false,
                    ExpirationDate = this.dateTime.UtcNow.AddDays(Constants.Common.TokenExpiration),
                    Message = string.Format(Constants.Common.Success, nameof(User), user.Username, "Registered")
                };
            }
        }
    }
}
