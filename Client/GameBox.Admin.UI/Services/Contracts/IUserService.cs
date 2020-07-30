using GameBox.Admin.UI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListModel>> GetUsers(string username);

        Task Lock(string username);

        Task Unlock(string username);

        Task AddRole(string username, string role);
        
        Task RemoveRole(string username, string role);

        Task CreateUser(UserFormModel user);
    }
}