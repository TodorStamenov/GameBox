using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;

namespace GameBox.Application.Categories.Queries.GetCategory;

public class GetCategoryQuery : IRequest<CategoryViewModel>
{
    public Guid Id { get; set; }

    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryViewModel>
    {
        private readonly IMapper mapper;
        private readonly IDataService context;

        public GetCategoryQueryHandler(IMapper mapper, IDataService context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<CategoryViewModel> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = this.context
                .All<Category>()
                .Where(c => c.Id == request.Id)
                .ProjectTo<CategoryViewModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            return await Task.FromResult(category);
        }
    }
}
