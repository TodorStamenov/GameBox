using GameBox.Core;
using GameBox.Services.Models.View.Users;
using System.Collections.Generic;

namespace GameBox.Services.Contracts
{
    public interface IUserService
    {
        ServiceResult AddRole(string username, string roleName);

        ServiceResult RemoveRole(string username, string roleName);

        ServiceResult Lock(string username);

        ServiceResult Unlock(string username);

        IEnumerable<ListUsersViewModel> All(string userQuery);
    }
}