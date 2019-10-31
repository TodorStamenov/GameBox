using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoriesListViewModel>>
    {
        private readonly IMapper mapper;
        private readonly IGameBoxDbContext context;

        public GetAllCategoriesQueryHandler(IMapper mapper, IGameBoxDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<CategoriesListViewModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await this.context
                .Categories
                .OrderBy(c => c.Name)
                .ProjectTo<CategoriesListViewModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
