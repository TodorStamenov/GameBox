using FluentValidation.Results;
using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Commands.UpdateGame
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, string>
    {
        private readonly IGameBoxDbContext context;

        public UpdateGameCommandHandler(IGameBoxDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            if (!DateTime.TryParse(request.ReleaseDate, out DateTime releaseDate))
            {
                throw new ValidationException(new List<ValidationFailure>
                {
                    new ValidationFailure(
                        nameof(UpdateGameCommand.ReleaseDate),
                        string.Format(Constants.Common.NotValidDateFormat, request.ReleaseDate))
                });
            }

            var game = await this.context
                .Games
                .FindAsync(request.Id);

            if (game == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            game.Title = request.Title;
            game.Description = request.Description;
            game.ThumbnailUrl = request.ThumbnailUrl;
            game.Price = request.Price;
            game.Size = request.Size;
            game.VideoId = request.VideoId;
            game.ReleaseDate = releaseDate;
            game.CategoryId = request.CategoryId;

            await this.context.SaveChangesAsync(cancellationToken);

            return string.Format(Constants.Common.Success, nameof(Game), request.Title, Constants.Common.Edited);
        }
    }
}
