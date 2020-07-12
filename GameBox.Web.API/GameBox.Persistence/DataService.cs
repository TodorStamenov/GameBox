using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameBox.Application.Contracts.Services;

namespace GameBox.Persistence
{
    public class DataService : IDataService
    {
        public DataService(GameBoxDbContext context)
        {
            Context = context;
        }

        public GameBoxDbContext Context { get; set; }

        public IQueryable<TEntity> All<TEntity>() where TEntity : class
        {
            return this.Context.Set<TEntity>().AsQueryable();
        }

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await this.Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await Task.FromResult(this.Context.Set<TEntity>().Update(entity));
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await Task.FromResult(this.Context.Set<TEntity>().Remove(entity));
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await this.Context.SaveChangesAsync();
        }
    }
}