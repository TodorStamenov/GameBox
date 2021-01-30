using GameBox.Application.Contracts.Services;
using GameBox.Application.Model;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace GameBox.Infrastructure
{
    public class QueueSenderService : IQueueSenderService
    {
        public void PostQueueMessage<T>(string queueName, T command) where T : QueueMessageModel
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "172.17.0.1",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
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
}
