using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using MediatR;

namespace GameBox.Application.Games.Queries.GetGamesByTitle;

public class GetGamesByTitleQuery : IRequest<IEnumerable<GamesListByTitleViewModel>>
{
    public string Title { get; set; }

    public class GetGamesByTitleQueryHandler : IRequestHandler<GetGamesByTitleQuery, IEnumerable<GamesListByTitleViewModel>>
    {
        private const int GamesOnPage = 15;

        private readonly IMapper mapper;
        private readonly IDataService context;

        public GetGamesByTitleQueryHandler(IMapper mapper, IDataService context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<GamesListByTitleViewModel>> Handle(GetGamesByTitleQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Game> query = this.context.All<Game>();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query
                    .Where(g => g.Title.ToLower().Contains(request.Title.ToLower()));
            }

            return await Task.FromResult(query
                .OrderBy(g => g.Title)
                .Take(GamesOnPage)
                .ProjectTo<GamesListByTitleViewModel>(this.mapper.ConfigurationProvider)
                .ToList());
        }
    }
}
