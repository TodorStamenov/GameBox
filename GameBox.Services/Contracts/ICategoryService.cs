using GameBox.Services.Models.View.Categories;
using System.Collections.Generic;

namespace GameBox.Services.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<ListCategoriesViewModel> All();
    }
}