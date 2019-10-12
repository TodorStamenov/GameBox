using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetGamesByTitle
{
    public class GetGamesByTitleQueryHandler : IRequestHandler<GetGamesByTitleQuery, IEnumerable<GamesListByTitleViewModel>>
    {
        private const int GamesOnPage = 15;

        private readonly IMapper mapper;
        private readonly IGameBoxDbContext context;

        public GetGamesByTitleQueryHandler(IMapper mapper, IGameBoxDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<GamesListByTitleViewModel>> Handle(GetGamesByTitleQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Game> query = this.context.Games;

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query
                    .Where(g => g.Title.ToLower().Contains(request.Title.ToLower()));
            }

            return await query
                .OrderBy(g => g.Title)
                .Take(GamesOnPage)
                .ProjectTo<GamesListByTitleViewModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
