using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<CategoryViewModel>
    {
        public Guid Id { get; set; }

        public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryViewModel>
        {
            private readonly IMapper mapper;
            private readonly IGameBoxDbContext context;

            public GetCategoryQueryHandler(IMapper mapper, IGameBoxDbContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<CategoryViewModel> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
            {
                var category = await this.context
                    .Set<Category>()
                    .Where(c => c.Id == request.Id)
                    .ProjectTo<CategoryViewModel>(this.mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

                if (category == null)
                {
                    throw new NotFoundException(nameof(Category), request.Id);
                }

                return category;
            }
        }
    }
}
