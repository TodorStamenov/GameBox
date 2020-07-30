using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Categories.Queries.GetMenuCategories
{
    public class GetMenuCategoriesQuery : IRequest<IEnumerable<CategoriesListMenuViewModel>>
    {
        public class GetMenuCategoriesQueryHandler : IRequestHandler<GetMenuCategoriesQuery, IEnumerable<CategoriesListMenuViewModel>>
        {
            private readonly IMapper mapper;
            private readonly IDataService context;

            public GetMenuCategoriesQueryHandler(IMapper mapper, IDataService context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<IEnumerable<CategoriesListMenuViewModel>> Handle(GetMenuCategoriesQuery request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(this.context
                    .All<Category>()
                    .OrderBy(c => c.Name)
                    .ProjectTo<CategoriesListMenuViewModel>(this.mapper.ConfigurationProvider)
                    .ToList());
            }
        }
    }
}