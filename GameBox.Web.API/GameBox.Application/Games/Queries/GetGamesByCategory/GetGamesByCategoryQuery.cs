﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts;
using GameBox.Application.Games.Queries.GetAllGames;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Games.Queries.GetGamesByCategory
{
    public class GetGamesByCategoryQuery : IRequest<IEnumerable<GamesListViewModel>>
    {
        public int LoadedGames { get; set; }

        public Guid CategoryId { get; set; }

        public class GetGamesByCategoryQueryHandler : IRequestHandler<GetGamesByCategoryQuery, IEnumerable<GamesListViewModel>>
        {
            private const int GameCardsCount = 9;

            private readonly IMapper mapper;
            private readonly IGameBoxDbContext context;

            public GetGamesByCategoryQueryHandler(IMapper mapper, IGameBoxDbContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<IEnumerable<GamesListViewModel>> Handle(GetGamesByCategoryQuery request, CancellationToken cancellationToken)
            {
                return await this.context
                    .Set<Game>()
                    .Where(g => g.CategoryId == request.CategoryId)
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
}
