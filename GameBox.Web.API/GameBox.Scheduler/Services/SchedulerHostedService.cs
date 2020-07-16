using Dapper;
using GameBox.Scheduler.Contracts;
using GameBox.Scheduler.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Scheduler.Services
{
    public class SchedulerHostedService : IHostedService, IDisposable
    {
        private Timer timer;
        private readonly IOptions<Settings> settings;
        private readonly IQueueSenderService queueService;

        public SchedulerHostedService(IOptions<Settings> settings, IQueueSenderService queueService)
        {
            this.settings = settings;
            this.queueService = queueService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(ProcessMessages, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.timer.Dispose();
        }

        private void ProcessMessages(object state)
        {
            try
            {
                using (var connection = new SqlConnection(this.settings.Value.ConnectionString))
                {
                    var messages = connection.Query<Message>("SELECT Id, QueueName, SerializedData [serializedData] FROM Messages WHERE Published = 0");

                    foreach (var message in messages)
                    {
                        this.queueService.PostQueueMessage(message.QueueName, message.SerializedData);
                        connection.Execute("UPDATE Messages SET Published = 1 WHERE Id = @MessageId", new { MessageId = message.Id });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Post messages on queue failed: {ex.Message}");
            }
        }
    }
}