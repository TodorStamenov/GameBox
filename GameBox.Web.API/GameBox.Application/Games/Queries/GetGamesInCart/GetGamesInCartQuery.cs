using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetGamesInCart
{
    public class GetGamesInCartQuery : IRequest<IEnumerable<GamesListCartViewModel>>
    {
        public IEnumerable<Guid> GameIds { get; set; }

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
                return await this.context
                    .Set<Game>()
                    .Where(g => request.GameIds.Contains(g.Id))
                    .OrderByDescending(g => g.Price)
                    .Distinct()
                    .ProjectTo<GamesListCartViewModel>(this.mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
