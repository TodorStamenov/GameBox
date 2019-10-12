using AutoMapper;
using GameBox.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetGame
{
    public class GetGameQueryHandler : IRequestHandler<GetGameQuery, GameViewModel>
    {
        private readonly IMapper mapper;
        private readonly IGameBoxDbContext context;

        public GetGameQueryHandler(IMapper mapper, IGameBoxDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<GameViewModel> Handle(GetGameQuery request, CancellationToken cancellationToken)
        {
            var game = await this.context
                .Games
                .Where(g => g.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return this.mapper.Map<GameViewModel>(game);
        }
    }
}
