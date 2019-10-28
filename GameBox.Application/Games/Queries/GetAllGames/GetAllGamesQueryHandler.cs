﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetAllGames
{
    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<GamesListViewModel>>
    {
        private const int GameCardsCount = 9;

        private readonly IMapper mapper;
        private readonly IGameBoxDbContext context;

        public GetAllGamesQueryHandler(IMapper mapper, IGameBoxDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<GamesListViewModel>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            return await this.context
                .Games
                .OrderByDescending(g => g.ReleaseDate)
                .ThenByDescending(g => g.ViewCount)
                .ThenBy(g => g.Title)
                .Skip(request.LoadedGames)
                .Take(GameCardsCount)
                .ProjectTo<GamesListViewModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}