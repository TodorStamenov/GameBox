using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Commands.DeleteGame;

public class DeleteGameCommand : IRequest<string>
{
    public Guid Id { get; set; }

    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, string>
    {
        private readonly IDataService context;

        public DeleteGameCommandHandler(IDataService context)
        {
            this.context = context;
        }

        public async Task<string> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var game = this.context
                .All<Game>()
                .Where(g => g.Id == request.Id)
                .FirstOrDefault();

            if (game == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            await this.context.DeleteAsync(game);
            await this.context.SaveAsync(cancellationToken);

            return string.Format(Constants.Common.Success, nameof(Game), game.Title, Constants.Common.Deleted);
        }
    }
}
