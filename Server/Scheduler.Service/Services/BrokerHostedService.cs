using Dapper;
using Scheduler.Service.Contracts;
using Scheduler.Service.Model;
using System.Data.SqlClient;

namespace Scheduler.Service.Services;

public class BrokerHostedService : BackgroundService
{
    private readonly string connectionString;
    private readonly ILogger<BrokerHostedService> logger;
    private readonly IQueueSenderService queueService;

    public BrokerHostedService(
        IConfiguration configuration,
        IQueueSenderService queueService,
        ILogger<BrokerHostedService> logger)
    {
        this.connectionString = configuration.GetConnectionString("Messages");
        this.logger = logger;
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
            using var connection = new SqlConnection(this.connectionString);
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
        catch (Exception ex)
        {
            this.logger.LogInformation("Post messages on queue failed: {Message}", ex.Message);
        }
    }
}
