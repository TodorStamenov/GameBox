using GameBox.Application.Contracts;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterViewModel>
    {
        private readonly IGameBoxDbContext context;
        private readonly IAccountService accountService;

        public RegisterCommandHandler(IGameBoxDbContext context, IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
        }

        public async Task<RegisterViewModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var salt = this.accountService.GenerateSalt();
            var hashedPassword = this.accountService.HashPassword(request.Password, salt);

            var user = new User
            {
                Username = request.Username,
                Password = hashedPassword,
                Salt = salt
            };

            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync(cancellationToken);

            var token = this.accountService.GenerateJwtToken(user.Username);

            return new RegisterViewModel
            {
                Username = user.Username,
                Token = token,
                IsAdmin = false,
                Message = string.Format(Constants.Common.Success, nameof(User), user.Username, "Registered")
            };
        }
    }
}