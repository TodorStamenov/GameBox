using GameBox.Core;

namespace GameBox.Services.Contracts
{
    public interface IAccountService
    {
        ServiceResult Login(string username, string password);

        ServiceResult Register(string username, string password);

        ServiceResult ChangePassword(string username, string oldPassword, string newPassword);
    }
}