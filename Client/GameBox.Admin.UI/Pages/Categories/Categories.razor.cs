using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Pages.Categories
{
    [Route("/categories/all")]
    public partial class Categories : ComponentBase
    {
        public IEnumerable<CategoryListModel> categories = new List<CategoryListModel>();

        [Inject] public ICategoryService CategoryService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.categories = await this.CategoryService.GetCategories();
        }
    }
}