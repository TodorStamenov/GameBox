using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly string categoriesUrl;
        private readonly IHttpClientService httpClient;

        public CategoryService(
            IHttpClientService httpClient,
            ConfigurationSettings config)
        {
            this.httpClient = httpClient;
            this.categoriesUrl = $"{config.GamesApiUrl}categories/";
        }

        public async Task<IEnumerable<CategoryListModel>> GetCategories()
        {
            return await this.httpClient.GetAsync<IEnumerable<CategoryListModel>>(this.categoriesUrl);
        }

        public async Task<IEnumerable<CategoryMenuModel>> GetCategoryNames()
        {
            return await this.httpClient.GetAsync<IEnumerable<CategoryMenuModel>>(this.categoriesUrl + "menu");
        }

        public async Task<CategoryFormModel> GetCategory(string categoryId)
        {
            return await this.httpClient.GetAsync<CategoryFormModel>(this.categoriesUrl + categoryId);
        }

        public async Task CreateCategory(CategoryFormModel category)
        {
            await this.httpClient.PostAsync<string>(this.categoriesUrl, category);
        }

        public async Task EditCategory(string categoryId, CategoryFormModel category)
        {
            await this.httpClient.PutAsync<string>(this.categoriesUrl + categoryId, category);
        }
    }
}