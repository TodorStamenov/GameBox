using GameBox.Application.Accounts.Queries.GenerateSalt;
using GameBox.Application.Accounts.Queries.HashPassword;
using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordViewModel>
    {
        private readonly IMediator mediator;
        private readonly IGameBoxDbContext context;

        public ChangePasswordCommandHandler(IMediator mediator, IGameBoxDbContext context)
        {
            this.mediator = mediator;
            this.context = context;
        }

        public async Task<ChangePasswordViewModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await this.context
                .Users
                .Where(u => u.Username == request.Username)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Username);
            }

            var oldHashedPassword = await this.mediator.Send(new HashPasswordQuery
            {
                Password = request.OldPassword,
                Salt = user.Salt
            });

            if (oldHashedPassword != user.Password)
            {
                throw new InvalidCredentialsException();
            }

            user.Salt = await this.mediator.Send(new GenerateSaltQuery());

            var newHashedPassword = await this.mediator.Send(new HashPasswordQuery
            {
                Password = request.NewPassword,
                Salt = user.Salt
            });

            user.Password = newHashedPassword;

            await this.context.SaveChangesAsync(cancellationToken);

            return new ChangePasswordViewModel { Message = "You have successfully updated your password!" };
        }
    }
}
