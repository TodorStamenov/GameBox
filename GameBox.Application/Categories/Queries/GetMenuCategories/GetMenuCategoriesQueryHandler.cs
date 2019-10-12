﻿using AutoMapper;
using GameBox.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Categories.Queries.GetMenuCategories
{
    public class GetMenuCategoriesQueryHandler : IRequestHandler<GetMenuCategoriesQuery, IEnumerable<CategoriesListMenuViewModel>>
    {
        private readonly IMapper mapper;
        private readonly IGameBoxDbContext context;

        public GetMenuCategoriesQueryHandler(IMapper mapper, IGameBoxDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<CategoriesListMenuViewModel>> Handle(GetMenuCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await this.context
                .Categories
                .Include(c => c.Games)
                .Where(c => c.Games.Any())
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken);

            return this.mapper.Map<IEnumerable<CategoriesListMenuViewModel>>(categories);
        }
    }
}