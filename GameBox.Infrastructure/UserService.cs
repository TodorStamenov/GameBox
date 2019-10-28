using GameBox.Application.Contracts;
using GameBox.Application.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameBox.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly IGameBoxDbContext context;

        public UserService(IGameBoxDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> GetUserIdAsync(string username)
        {
            return await this.context
                .Users
                .Where(u => u.Username == username)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
        }
    }
}
