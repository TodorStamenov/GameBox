using GameBox.Application.Accounts.Queries.GenerateJwtToken;
using GameBox.Application.Accounts.Queries.GenerateSalt;
using GameBox.Application.Accounts.Queries.HashPassword;
using GameBox.Application.Contracts;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterViewModel>
    {
        private readonly IMediator mediator;
        private readonly IGameBoxDbContext context;

        public RegisterCommandHandler(IMediator mediator, IGameBoxDbContext context)
        {
            this.mediator = mediator;
            this.context = context;
        }

        public async Task<RegisterViewModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var salt = await this.mediator.Send(new GenerateSaltQuery());
            var hashedPassword = await this.mediator.Send(new HashPasswordQuery
            {
                Password = request.Password,
                Salt = salt
            });

            var user = new User
            {
                Username = request.Username,
                Password = hashedPassword,
                Salt = salt
            };

            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync(cancellationToken);

            var token = await this.mediator.Send(new GenerateJwtTokenQuery
            {
                Id = user.Id.ToString(),
                Username = user.Username
            });

            return new RegisterViewModel
            {
                Username = user.Username,
                Token = token,
                IsAdmin = false,
                ExpirationDate = DateTime.UtcNow.AddDays(Constants.Common.TokenExpiration),
                Message = string.Format(Constants.Common.Success, nameof(User), user.Username, "Registered")
            };
        }
    }
}