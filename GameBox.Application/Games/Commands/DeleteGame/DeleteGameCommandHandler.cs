using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Commands.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, string>
    {
        private readonly IGameBoxDbContext context;

        public DeleteGameCommandHandler(IGameBoxDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var game = await this.context
                .Games
                .FindAsync(request.Id);

            if (game == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            this.context.Games.Remove(game);
            await this.context.SaveChangesAsync(cancellationToken);

            return string.Format(Constants.Common.Success, nameof(Game), game.Title, Constants.Common.Deleted);
        }
    }
}
