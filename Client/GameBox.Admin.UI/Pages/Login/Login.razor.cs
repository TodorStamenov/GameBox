using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Pages.Login
{
    public partial class Login : ComponentBase
    {
        public LoginFormModel loginForm = new LoginFormModel();

        [Inject] public IAuthService AuthService { get; set; }

        [Inject] public NavigationManager Router { get; set; }

        public async Task OnLoginAsync()
        {
            await this.AuthService.LoginAsync(this.loginForm);
            this.Router.NavigateTo("/");
        }
    }
}