using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Contracts
{
    public interface IGameBoxDbContext
    {
        DbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
