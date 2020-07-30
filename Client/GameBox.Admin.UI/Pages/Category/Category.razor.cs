using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Pages.Category
{
    [Route("/categories/{action}")]
    public partial class Category : ComponentBase
    {
        private string categoryId;
        public CategoryFormModel categoryForm = new CategoryFormModel();

        [Parameter] public string Action { get; set; }

        [Inject] public ICategoryService CategoryService { get; set; }

        [Inject] public NavigationManager Router { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.categoryId = this.Router.GetQueryParam("categoryId");
            if (this.Action.ToLower() == "edit" && !string.IsNullOrWhiteSpace(this.categoryId))
            {
                this.categoryForm = await this.CategoryService.GetCategory(this.categoryId);
            }
        }

        public async Task OnSaveCategoryAsync()
        {
            if (this.Action.ToLower() == "edit" && !string.IsNullOrWhiteSpace(this.categoryId))
            {
                await this.CategoryService.EditCategory(this.categoryId, this.categoryForm);
            }
            else if (this.Action.ToLower() == "create")
            {
                await this.CategoryService.CreateCategory(this.categoryForm);
            }

            this.Router.NavigateTo("/categories/all");
        }
    }
}