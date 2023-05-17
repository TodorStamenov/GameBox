using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;

namespace GameBox.Application.Games.Queries.GetGame;

public class GetGameQuery : IRequest<GameViewModel>
{
    public Guid Id { get; set; }

    public class GetGameQueryHandler : IRequestHandler<GetGameQuery, GameViewModel>
    {
        private readonly IMapper mapper;
        private readonly IDataService context;

        public GetGameQueryHandler(IMapper mapper, IDataService context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<GameViewModel> Handle(GetGameQuery request, CancellationToken cancellationToken)
        {
            var game = this.context
                .All<Game>()
                .Where(g => g.Id == request.Id)
                .ProjectTo<GameViewModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

            if (game == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            return await Task.FromResult(game);
        }
    }
}
