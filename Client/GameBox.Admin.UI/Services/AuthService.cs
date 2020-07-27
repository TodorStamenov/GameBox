using Blazored.LocalStorage;
using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services
{
    public class AuthService : IAuthService
    {
        public event Func<Task> OnUserUpdatedAsync;

        private UserModel currentUser;

        private readonly string authUrl;
        private readonly HttpClient http;
        private readonly ILocalStorageService localStorage;

        public AuthService(
            HttpClient http,
            ILocalStorageService localStorage,
            ConfigurationSettings config)
        {
            this.http = http;
            this.localStorage = localStorage;
            this.authUrl = $"{config.GameBoxApiUrl}account/";
        }

        public async Task<UserModel> GetCurrentUser()
        {
            if (this.currentUser != null)
            {
                return this.currentUser;
            }

            if (await this.localStorage.ContainKeyAsync("currentUser"))
            {                
                return await this.localStorage.GetItemAsync<UserModel>("currentUser");
            }

            return null;
        }

        public async Task<bool> IsAuthed()
        {
            var user = await this.GetCurrentUser();
            return user != null && DateTime.Now < user.ExpirationDate;
        }

        public async Task LoginAsync(LoginFormModel body)
        {
            var result = await this.http.PostAsJsonAsync($"{this.authUrl}login", body);

            result.EnsureSuccessStatusCode();
            this.currentUser = await result.Content.ReadFromJsonAsync<UserModel>();

            await this.localStorage.SetItemAsync("currentUser", this.currentUser);
            await this.OnUserUpdatedAsync?.Invoke();
        }

        public async Task LogoutAsync()
        {
            this.currentUser = null;
            await this.localStorage.ClearAsync();
            await this.OnUserUpdatedAsync?.Invoke();
        }
    }
}