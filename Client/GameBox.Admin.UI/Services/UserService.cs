using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services
{
    public class UserService : IUserService
    {
        private readonly string usersUrl;
        private readonly IHttpClientService httpClient;

        public UserService(
            IHttpClientService httpClient,
            ConfigurationSettings config)
        {
            this.httpClient = httpClient;
            this.usersUrl = $"{config.GameBoxApiUrl}users/";
        }

        public async Task<IEnumerable<UserListModel>> GetUsers(string username)
        {
            return await this.httpClient.GetAsync<IEnumerable<UserListModel>>($"{this.usersUrl}all?username={username}");
        }

        public async Task Lock(string username)
        {
            await this.httpClient.GetAsync<string>($"{this.usersUrl}lock?username={username}");
        }

        public async Task Unlock(string username)
        {
            await this.httpClient.GetAsync<string>($"{this.usersUrl}unlock?username={username}");
        }

        public async Task AddRole(string username, string role)
        {
            await this.httpClient.GetAsync<string>($"{this.usersUrl}addRole?username={username}&roleName={role}");
        }

        public async Task RemoveRole(string username, string role)
        {
            await this.httpClient.GetAsync<string>($"{this.usersUrl}removeRole?username={username}&roleName={role}");
        }

        public async Task CreateUser(UserFormModel user)
        {
            await this.httpClient.PostAsync<string>(this.usersUrl + "create", user);
        }
    }
}