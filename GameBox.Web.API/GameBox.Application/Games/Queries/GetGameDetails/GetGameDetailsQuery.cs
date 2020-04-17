using AutoMapper;
using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetGameDetails
{
    public class GetGameDetailsQuery : IRequest<GameDetailsViewModel>
    {
        public Guid Id { get; set; }

        public class GetGameDetailsQueryHandler : IRequestHandler<GetGameDetailsQuery, GameDetailsViewModel>
        {
            private readonly IMapper mapper;
            private readonly IGameBoxDbContext context;

            public GetGameDetailsQueryHandler(IMapper mapper, IGameBoxDbContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<GameDetailsViewModel> Handle(GetGameDetailsQuery request, CancellationToken cancellationToken)
            {
                var game = await this.context
                    .Games
                    .FindAsync(request.Id);

                if (game == null)
                {
                    throw new NotFoundException(nameof(Game), request.Id);
                }

                game.ViewCount++;

                await this.context.SaveChangesAsync(cancellationToken);

                return this.mapper.Map<GameDetailsViewModel>(game);
            }
        }
    }
}
