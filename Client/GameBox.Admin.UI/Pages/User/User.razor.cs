using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Pages.User
{
    [Route("/users/create")]
    public partial class User : ComponentBase
    {
        public UserFormModel userForm = new UserFormModel();

        [Inject] public IUserService UserService { get; set; }

        [Inject] public NavigationManager Router { get; set; }

        public async Task OnCreateUser()
        {
            await this.UserService.CreateUser(this.userForm);
            this.Router.NavigateTo("/users/all");
        }
    }
}