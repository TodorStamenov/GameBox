using GameBox.Admin.UI.Model;
using System;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services.Contracts
{
    public interface IAuthService
    {
        event Func<Task> OnUserUpdatedAsync;

        Task<bool> IsAuthed();

        Task<UserModel> GetCurrentUser();

        Task LoginAsync(LoginFormModel login);

        Task LogoutAsync();
    }
}