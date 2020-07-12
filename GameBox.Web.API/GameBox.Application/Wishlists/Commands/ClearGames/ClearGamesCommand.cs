using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Wishlists.Commands.ClearGames
{
    public class ClearGamesCommand : IRequest<IEnumerable<Guid>>
    {
        public Guid UserId { get; set; }

        public class ClearGamesCommandHandler : IRequestHandler<ClearGamesCommand, IEnumerable<Guid>>
        {
            private readonly IDataService context;

            public ClearGamesCommandHandler(IDataService context)
            {
                this.context = context;
            }

            public async Task<IEnumerable<Guid>> Handle(ClearGamesCommand request, CancellationToken cancellationToken)
            {
                var result = new List<Guid>();

                var games = this.context
                    .All<Wishlist>()
                    .Where(w => w.UserId == request.UserId)
                    .ToList();

                foreach (var game in games)
                {
                    await this.context.DeleteAsync(game);
                    result.Add(game.GameId);
                }

                await this.context.SaveAsync(cancellationToken);

                return result;
            }
        }
    }
}
