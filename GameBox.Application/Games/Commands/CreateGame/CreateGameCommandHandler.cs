﻿using FluentValidation.Results;
using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Commands.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, string>
    {
        private readonly IGameBoxDbContext context;

        public CreateGameCommandHandler(IGameBoxDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            if (!DateTime.TryParse(request.ReleaseDate, out DateTime releaseDate))
            {
                throw new ValidationException(new List<ValidationFailure>
                {
                    new ValidationFailure(
                        nameof(CreateGameCommand.ReleaseDate),
                        string.Format(Constants.Common.NotValidDateFormat, request.ReleaseDate))
                });
            }

            await this.context.Games.AddAsync(new Game
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

            await this.context.SaveChangesAsync(cancellationToken);

            return string.Format(Constants.Common.Success, nameof(Game), request.Title, Constants.Common.Added);
        }
    }
}