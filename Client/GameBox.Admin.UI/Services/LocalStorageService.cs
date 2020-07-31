using GameBox.Admin.UI.Services.Contracts;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task<bool> ContainsKeyAsync(string key)
        {
            return await this.jsRuntime.InvokeAsync<bool>("containsKey", key);;
        }

        public async Task<T> GetItemAsync<T>(string key)
        {
            var value = await this.jsRuntime.InvokeAsync<string>("getItem", key);
            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetItemAsync<T>(string key, T value)
        {
            var serializedObject = JsonSerializer.Serialize(value);
            await this.jsRuntime.InvokeVoidAsync("setItem", key, serializedObject);
        }

        public async Task ClearAsync()
        {
            await this.jsRuntime.InvokeVoidAsync("clear");
        }
    }
}