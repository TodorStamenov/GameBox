using Dapper;
using GameBox.Scheduler.Contracts;
using GameBox.Scheduler.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Scheduler.Services
{
    public class SchedulerHostedService : BackgroundService
    {
        private readonly Settings settings;
        private readonly ILogger<SchedulerHostedService> logger;
        private readonly IQueueSenderService queueService;

        public SchedulerHostedService(
            Settings settings,
            ILogger<SchedulerHostedService> logger,
            IQueueSenderService queueService)
        {
            this.settings = settings;
            this.logger = logger;
            this.queueService = queueService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await this.ProcessMessages();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task ProcessMessages()
        {
            try
            {
                using (var connection = new SqlConnection(this.settings.ConnectionString))
                {
                    var messages = await connection.QueryAsync<Message>("SELECT Id, QueueName, SerializedData [serializedData] FROM Messages WHERE Published = 0");

                    foreach (var message in messages)
                    {
                        this.queueService.PostQueueMessage(message.QueueName, message.SerializedData);
                        await connection.ExecuteAsync("UPDATE Messages SET Published = 1 WHERE Id = @MessageId", new { MessageId = message.Id });
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Post messages on queue failed: {ex.Message}");
            }
        }
    }
}