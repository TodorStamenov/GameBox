using MediatR;
using System;

namespace GameBox.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<CategoryViewModel>
    {
        public Guid Id { get; set; }
    }
}
