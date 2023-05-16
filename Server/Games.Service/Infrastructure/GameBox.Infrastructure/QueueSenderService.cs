using GameBox.Application.Contracts.Services;
using GameBox.Application.Model;
using GameBox.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace GameBox.Infrastructure;

public class QueueSenderService : IQueueSenderService
{
    private readonly RabbitMQSettings settings;

    public QueueSenderService(IOptions<RabbitMQSettings> settings)
    {
        this.settings = settings.Value;
    }

    public void PostQueueMessage<T>(string queueName, T command) where T : QueueMessageModel
    {
        var connectionFactory = new ConnectionFactory
        {
            Port = this.settings.Port,
            HostName = this.settings.Host,
            UserName = this.settings.Username,
            Password = this.settings.Password,
            RequestedConnectionTimeout = TimeSpan.FromMilliseconds(3000)
        };

        using (var rabbitConnection = connectionFactory.CreateConnection())
        {
            using (var channel = rabbitConnection.CreateModel())
            {
                var message = JsonSerializer.Serialize(command);
                var body = Encoding.UTF8.GetBytes(message);

                channel.QueueDeclare(
                    queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: true,
                    arguments: null);

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: queueName,
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
