using FluentValidation.Results;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;

namespace GameBox.Application.Games.Commands.UpdateGame;

public class UpdateGameCommand : IRequest<string>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public decimal Price { get; set; }

    public double Size { get; set; }

    public string VideoId { get; set; }

    public string ThumbnailUrl { get; set; }

    public string Description { get; set; }

    public string ReleaseDate { get; set; }

    public Guid CategoryId { get; set; }

    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, string>
    {
        private readonly IDataService context;

        public UpdateGameCommandHandler(IDataService context)
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

            var game = this.context
                .All<Game>()
                .Where(g => g.Id == request.Id)
                .FirstOrDefault();

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

            await this.context.SaveAsync(cancellationToken);

            return string.Format(Constants.Common.Success, nameof(Game), request.Title, Constants.Common.Edited);
        }
    }
}
