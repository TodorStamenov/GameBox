using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using Hangfire;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Infrastructure
{
    public class MessagesHostedService : IHostedService
    {
        private readonly IDataService context;
        private readonly IMessageQueueSenderService queue;
        private readonly IRecurringJobManager recurringJob;

        public MessagesHostedService(
            IDataService context,
            IMessageQueueSenderService queue,
            IRecurringJobManager recurringJob)
        {
            this.context = context;
            this.queue = queue;
            this.recurringJob = recurringJob;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.recurringJob.AddOrUpdate(
                nameof(MessagesHostedService),
                () => this.ProcessPendingMessages(),
                "*/5 * * * * *");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void ProcessPendingMessages()
        {
            var messages = this.context
                .All<Message>()
                .Where(m => !m.Published)
                .ToList();

            foreach (var message in messages)
            {
                this.queue.Send(message.QueueName, message.Data);
                message.MarkAsPublished();
                this.context.SaveAsync();
            }
        }
    }
}