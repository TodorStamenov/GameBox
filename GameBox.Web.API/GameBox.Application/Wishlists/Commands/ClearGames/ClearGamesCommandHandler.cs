using GameBox.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Wishlists.Commands.ClearGames
{
    public class ClearGamesCommandHandler : IRequestHandler<ClearGamesCommand, IEnumerable<Guid>>
    {
        private readonly IGameBoxDbContext context;

        public ClearGamesCommandHandler(IGameBoxDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Guid>> Handle(ClearGamesCommand request, CancellationToken cancellationToken)
        {
            var result = new List<Guid>();

            var games = await this.context
                .Wishlists
                .Where(w => w.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            foreach (var game in games)
            {
                this.context.Wishlists.Remove(game);

                result.Add(game.GameId);
            }

            await this.context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
