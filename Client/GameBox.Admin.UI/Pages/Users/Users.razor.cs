using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Pages.Users
{
    [Route("/users/all")]
    public partial class Users : ComponentBase
    {
        public IEnumerable<UserListModel> users = new List<UserListModel>();

        public string Username { get; set; }

        [Inject] public IUserService UserService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.GetUsers(string.Empty);
        }

        public async Task OnLock(string username)
        {
            await this.UserService.Lock(username);
            await this.GetUsers(this.Username);
        }

        public async Task OnUnlock(string username)
        {
            await this.UserService.Unlock(username);
            await this.GetUsers(this.Username);
        }

        public async Task OnAddRole(string username, string role)
        {
            await this.UserService.AddRole(username, role);
            await this.GetUsers(this.Username);
        }

        public async Task OnRemoveRole(string username, string role)
        {
            await this.UserService.RemoveRole(username, role);
            await this.GetUsers(this.Username);
        }

        public async Task SearchUsers(string username)
        {
            this.Username = username;
            await this.GetUsers(username);
        }

        private async Task GetUsers(string username = "")
        {
            this.users = await this.UserService.GetUsers(username);
        }
    }
}