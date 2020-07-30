using GameBox.Admin.UI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListModel>> GetCategories();

        Task<IEnumerable<CategoryMenuModel>> GetCategoryNames();

        Task<CategoryFormModel> GetCategory(string categoryId);

        Task CreateCategory(CategoryFormModel category);

        Task EditCategory(string categoryId, CategoryFormModel category);
    }
}