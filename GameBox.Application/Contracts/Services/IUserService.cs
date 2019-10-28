using System;
using System.Threading.Tasks;

namespace GameBox.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<Guid> GetUserIdAsync(string username);
    }
}
