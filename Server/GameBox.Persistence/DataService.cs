using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task MarkMessageAsPublished<TKey>(TKey id)
        {
            var message = await this.Context.Set<Message>().FindAsync(id);
            message.MarkAsPublished();
            await this.Context.SaveChangesAsync();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default, params Message[] messages)
        {
            foreach (var message in messages)
            {
                this.Context.Set<Message>().Add(message);
            }

            return await this.Context.SaveChangesAsync();
        }
    }
}