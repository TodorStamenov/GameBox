using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
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
            private readonly IDataService context;

            public ChangePasswordCommandHandler(IAccountService account, IDataService context)
            {
                this.account = account;
                this.context = context;
            }

            public async Task<ChangePasswordViewModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var user = this.context
                    .All<User>()
                    .Where(u => u.Username == request.Username)
                    .FirstOrDefault();

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

                await this.context.SaveAsync(cancellationToken);

                return new ChangePasswordViewModel { Message = "You have successfully updated your password!" };
            }
        }
    }
}
