using GameBox.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Contracts.Services
{
    public interface IDataService : ITransientService
    {
        IQueryable<TEntity> All<TEntity>() where TEntity : class;

        Task AddAsync<TEntity>(TEntity entity) where TEntity : class;

        Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class;

        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class;

        Task MarkMessageAsPublished<TKey>(TKey id);

        Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken), params Message[] messages);
    }
}
