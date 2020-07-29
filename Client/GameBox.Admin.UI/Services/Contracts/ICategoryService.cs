using GameBox.Admin.UI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryMenuModel>> GetCategoryNames();
    }
}