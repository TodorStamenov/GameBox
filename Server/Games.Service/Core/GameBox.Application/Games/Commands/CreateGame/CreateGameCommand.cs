using FluentValidation.Results;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Commands.CreateGame;

public class CreateGameCommand : IRequest<string>
{
    public string Title { get; set; }

    public decimal Price { get; set; }

    public double Size { get; set; }

    public string VideoId { get; set; }

    public string ThumbnailUrl { get; set; }

    public string Description { get; set; }

    public string ReleaseDate { get; set; }

    public Guid CategoryId { get; set; }

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, string>
    {
        private readonly IDataService context;
        private readonly IQueueSenderService queue;

        public CreateGameCommandHandler(
            IDataService context,
            IQueueSenderService queue)
        {
            this.context = context;
            this.queue = queue;
        }

        public async Task<string> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            if (!DateTime.TryParse(request.ReleaseDate, out DateTime releaseDate))
            {
                throw new ValidationException(new List<ValidationFailure>
                    {
                        new ValidationFailure(
                            nameof(ReleaseDate),
                            string.Format(Constants.Common.NotValidDateFormat, request.ReleaseDate))
                    });
            }

            await this.context.AddAsync(new Game
            {
                Title = request.Title,
                Description = request.Description,
                ThumbnailUrl = request.ThumbnailUrl,
                Price = request.Price,
                Size = request.Size,
                VideoId = request.VideoId,
                ReleaseDate = releaseDate,
                CategoryId = request.CategoryId
            });

            var queueName = "games";
            var messageData = new GameCreatedMessage { Title = request.Title };

            var messageId = await this.context.SaveAsync(queueName, messageData, cancellationToken);
            this.queue.PostQueueMessage(queueName, messageData);
            await this.context.MarkMessageAsPublished(messageId);

            return string.Format(Constants.Common.Success, nameof(Game), request.Title, Constants.Common.Added);
        }
    }
}
