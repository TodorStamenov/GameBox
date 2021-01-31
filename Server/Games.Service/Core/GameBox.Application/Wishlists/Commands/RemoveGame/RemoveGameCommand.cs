﻿using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Wishlists.Commands.RemoveGame
{
    public class RemoveGameCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        public class RemoveGameCommandHandler : IRequestHandler<RemoveGameCommand, Guid>
        {
            private readonly IDataService context;

            public RemoveGameCommandHandler(IDataService context)
            {
                this.context = context;
            }

            public async Task<Guid> Handle(RemoveGameCommand request, CancellationToken cancellationToken)
            {
                var wishlist = this.context
                    .All<Wishlist>()
                    .Where(w => w.UserId == request.UserId)
                    .Where(w => w.GameId == request.GameId)
                    .FirstOrDefault();

                if (wishlist == null)
                {
                    throw new NotFoundException(nameof(Game), request.GameId);
                }

                await this.context.DeleteAsync(wishlist);
                await this.context.SaveAsync(cancellationToken);

                return request.GameId;
            }
        }
    }
}