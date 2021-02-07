using GameBox.Application.Contracts.Services;
using GameBox.Application.Model;
using Message.DataAccess;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Persistence
{
    public class DataService : IDataService
    {
        private readonly GameDbContext games;
        private readonly MessageDbContext messages;

        public DataService(
            GameDbContext games,
            MessageDbContext messages)
        {
            this.games = games;
            this.messages = messages;
        }

        public IQueryable<TEntity> All<TEntity>() where TEntity : class
        {
            return this.games.Set<TEntity>().AsQueryable();
        }

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await this.games.Set<TEntity>().AddAsync(entity);
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await Task.FromResult(this.games.Set<TEntity>().Update(entity));
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await Task.FromResult(this.games.Set<TEntity>().Remove(entity));
        }

        public async Task MarkMessageAsPublished<TKey>(TKey id)
        {
            var message = await this.messages.Messages.FindAsync(id);
            message.MarkAsPublished();
            await this.messages.SaveChangesAsync();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await this.games.SaveChangesAsync(cancellationToken);
        }

        public async Task<Guid> SaveAsync(string queueName, QueueMessageModel queueMessage, CancellationToken cancellationToken = default)
        {
            var message = new Message.DataAccess.Models.Message(queueName, queueMessage);

            await this.messages.Messages.AddAsync(message);
            await this.messages.SaveChangesAsync(cancellationToken);
            await this.games.SaveChangesAsync(cancellationToken);

            return message.Id;
        }
    }
}