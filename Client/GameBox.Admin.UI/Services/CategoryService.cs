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
            this.categoriesUrl = $"{config.GameBoxApiUrl}categories/";
        }

        public async Task<IEnumerable<CategoryMenuModel>> GetCategoryNames()
        {
            return await this.httpClient.GetAsync<IEnumerable<CategoryMenuModel>>(this.categoriesUrl + "menu");
        }
    }
}