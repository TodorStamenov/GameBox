using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Pages.Login
{
    [Route("/auth/login")]
    public partial class Login : ComponentBase
    {
        public LoginFormModel loginForm = new LoginFormModel();

        [Inject] public IAuthService AuthService { get; set; }

        public async Task OnLogin()
        {
            await this.AuthService.Login(this.loginForm);
        }
    }
}