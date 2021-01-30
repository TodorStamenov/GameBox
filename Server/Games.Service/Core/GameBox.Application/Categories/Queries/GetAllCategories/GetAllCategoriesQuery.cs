using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoriesListViewModel>>
    {
        public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoriesListViewModel>>
        {
            private readonly IMapper mapper;
            private readonly IDataService context;

            public GetAllCategoriesQueryHandler(IMapper mapper, IDataService context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<IEnumerable<CategoriesListViewModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(this.context
                    .All<Category>()
                    .OrderBy(c => c.Name)
                    .ProjectTo<CategoriesListViewModel>(this.mapper.ConfigurationProvider)
                    .ToList());
            }
        }
    }
}