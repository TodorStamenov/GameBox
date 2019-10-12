using MediatR;
using System.Collections.Generic;

namespace GameBox.Application.Categories.Queries.GetMenuCategories
{
    public class GetMenuCategoriesQuery : IRequest<IEnumerable<CategoriesListMenuViewModel>>
    {
    }
}