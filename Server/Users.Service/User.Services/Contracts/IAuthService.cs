using System.Threading.Tasks;
using User.Services.BindingModels.Auth;
using User.Services.Contracts.ServiceTypes;
using User.Services.ViewModels.Auth;

namespace User.Services.Contracts
{
    public interface IAuthService : IScopedService
    {
        Task<LoginViewModel> LoginAsync(LoginBindingModel model);

        Task<RegisterViewModel> RegisterAsync(RegisterBindingModel model);

        Task<ChangePasswordViewModel> ChangePasswordAsync(ChangePasswordBindingModel model);

        byte[] GenerateSalt();

        string HashPassword(string password, byte[] salt);

        string GenerateJwtToken(string id, string username, bool isAdmin);
    }
}
