using MediatR;
using System.Collections.Generic;

namespace GameBox.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoriesListViewModel>>
    {
    }
}