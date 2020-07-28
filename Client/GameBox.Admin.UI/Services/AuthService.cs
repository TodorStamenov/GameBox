using Blazored.LocalStorage;
using Blazored.Toast.Services;
using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services
{
    public class AuthService : IAuthService
    {
        public event Func<Task> OnUserUpdatedAsync;

        private UserModel currentUser;

        private readonly string authUrl;
        private readonly IHttpClientService http;
        private readonly NavigationManager router;
        private readonly IToastService toastService;
        private readonly ILocalStorageService localStorage;

        public AuthService(
            IHttpClientService http,
            NavigationManager router,
            IToastService toastService,
            ILocalStorageService localStorage,
            ConfigurationSettings config)
        {
            this.http = http;
            this.router = router;
            this.toastService = toastService;
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
                var user = await this.localStorage.GetItemAsync<UserModel>("currentUser");
                this.currentUser = user;

                return user;
            }

            return null;
        }

        public async Task<bool> IsAuthed()
        {
            var user = await this.GetCurrentUser();
            return user != null && DateTime.Now < user.ExpirationDate;
        }

        public async Task<bool> IsAdmin()
        {
            return await this.IsAuthed() && this.currentUser.IsAdmin;
        }

        public async Task LoginAsync(LoginFormModel body)
        {
            var user = await this.http.PostAsync<UserModel>($"{this.authUrl}login", body);

            if (!user.IsAdmin)
            {
                this.toastService.ShowError($"{user?.Username} is not in role Admin!");
                return;
            }

            this.currentUser = user;
            await this.localStorage.SetItemAsync("currentUser", this.currentUser);
            await this.OnUserUpdatedAsync?.Invoke();
            this.router.NavigateTo("/games/all");
        }

        public async Task LogoutAsync()
        {
            this.toastService.ShowSuccess($"{this.currentUser.Username} logged out successfully!");
            this.currentUser = null;
            await this.localStorage.ClearAsync();
            await this.OnUserUpdatedAsync?.Invoke();
            this.router.NavigateTo("/auth/login");
        }
    }
}