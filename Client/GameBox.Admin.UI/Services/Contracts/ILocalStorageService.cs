using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services.Contracts
{
    public interface ILocalStorageService
    {
        Task<bool> ContainsKeyAsync(string key);

        Task<T> GetItemAsync<T>(string key);

        Task SetItemAsync<T>(string key, T value);

        Task ClearAsync();
    }
}