using AutoMapper;
using GameBox.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetGamesInCart
{
    public class GetGamesInCartQueryHandler : IRequestHandler<GetGamesInCartQuery, IEnumerable<GamesListCartViewModel>>
    {
        private readonly IMapper mapper;
        private readonly IGameBoxDbContext context;

        public GetGamesInCartQueryHandler(IMapper mapper, IGameBoxDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<GamesListCartViewModel>> Handle(GetGamesInCartQuery request, CancellationToken cancellationToken)
        {
            var games = await this.context
                .Games
                .Where(g => request.GameIds.Contains(g.Id))
                .OrderByDescending(g => g.Price)
                .Distinct()
                .ToListAsync(cancellationToken);

            return this.mapper.Map<IEnumerable<GamesListCartViewModel>>(games);
        }
    }
}
