using System.Collections.Generic;
using System.Threading.Tasks;
using User.Services.Contracts.ServiceTypes;
using User.Services.ViewModels.Users;

namespace User.Services.Contracts
{
    public interface IUserService : IScopedService
    {
        Task<IEnumerable<UsersListViewModel>> GetAllUsersAsync(string username);

        Task<string> LockAsync(string username);

        Task<string> UnockAsync(string username);

        Task<string> AddRoleAsync(string username, string roleName);

        Task<string> RemoveRoleAsync(string username, string roleName);
    }
}
