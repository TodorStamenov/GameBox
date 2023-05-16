using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace GameBox.Application.Categories.Queries.GetMenuCategories;

public class GetMenuCategoriesQuery : IRequest<IEnumerable<CategoriesListMenuViewModel>>
{
    public class GetMenuCategoriesQueryHandler : IRequestHandler<GetMenuCategoriesQuery, IEnumerable<CategoriesListMenuViewModel>>
    {
        private readonly IMapper mapper;
        private readonly IDataService context;
        private readonly IMemoryCache cache;

        public GetMenuCategoriesQueryHandler(
            IMapper mapper,
            IDataService context,
            IMemoryCache cache)
        {
            this.mapper = mapper;
            this.context = context;
            this.cache = cache;
        }

        public async Task<IEnumerable<CategoriesListMenuViewModel>> Handle(GetMenuCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await this.cache.GetOrCreateAsync(Constants.Caching.CategoryMenuItemsKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Constants.Caching.CategoryMenuItemsLifeSpan);

                return Task.FromResult(this.context
                    .All<Category>()
                    .OrderBy(c => c.Name)
                    .ProjectTo<CategoriesListMenuViewModel>(this.mapper.ConfigurationProvider)
                    .ToList());
            });
        }
    }
}
