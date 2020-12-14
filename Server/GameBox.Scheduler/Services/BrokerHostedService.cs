using Dapper;
using GameBox.Scheduler.Contracts;
using GameBox.Scheduler.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Scheduler.Services
{
    public class BrokerHostedService : BackgroundService
    {
        private readonly string connectionString;
        private readonly RabbitMQSettings settings;
        private readonly ILogger<BrokerHostedService> logger;
        private readonly IQueueSenderService queueService;

        public BrokerHostedService(
            IConfiguration configuration,
            IQueueSenderService queueService,
            IOptions<RabbitMQSettings> settings,
            ILogger<BrokerHostedService> logger)
        {
            this.connectionString = configuration.GetConnectionString("Database");
            this.logger = logger;
            this.settings = settings.Value;
            this.queueService = queueService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await this.ProcessMessagesAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task ProcessMessagesAsync()
        {
            try
            {
                using (var connection = new SqlConnection(this.connectionString))
                {
                    var messages = await connection.QueryAsync<Message>(
                        @"SELECT Id, QueueName, SerializedData [serializedData]
                            FROM Messages
                           WHERE Published = 0");

                    foreach (var message in messages)
                    {
                        this.queueService.PostQueueMessage(message.QueueName, message.SerializedData);
                        await connection.ExecuteAsync(
                            "UPDATE Messages SET Published = 1 WHERE Id = @MessageId",
                            new { MessageId = message.Id });
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