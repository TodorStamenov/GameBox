using AutoMapper;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;

namespace GameBox.Application.Games.Queries.GetGameDetails;

public class GetGameDetailsQuery : IRequest<GameDetailsViewModel>
{
    public Guid Id { get; set; }

    public class GetGameDetailsQueryHandler : IRequestHandler<GetGameDetailsQuery, GameDetailsViewModel>
    {
        private readonly IMapper mapper;
        private readonly IDataService context;

        public GetGameDetailsQueryHandler(IMapper mapper, IDataService context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<GameDetailsViewModel> Handle(GetGameDetailsQuery request, CancellationToken cancellationToken)
        {
            var game = this.context
                .All<Game>()
                .Where(g => g.Id == request.Id)
                .FirstOrDefault();

            if (game == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            game.ViewCount++;

            await this.context.SaveAsync(cancellationToken);

            return this.mapper.Map<GameDetailsViewModel>(game);
        }
    }
}
