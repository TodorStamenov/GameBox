using GameBox.Scheduler.Contracts;
using GameBox.Scheduler.Model;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;

namespace GameBox.Scheduler.Services
{
    public class QueueSenderService : IQueueSenderService
    {
        private readonly IOptions<Settings> settings;
        
        public QueueSenderService(IOptions<Settings> settings)
        {
            this.settings = settings;
        }

        public void PostQueueMessage(string queueName, string message)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = this.settings.Value.RabbitMQHost,
                UserName = this.settings.Value.RabbitMQUsername,
                Password = this.settings.Value.RabbitMQPassword,
                Port = this.settings.Value.RabbitMQPort,
                RequestedConnectionTimeout = TimeSpan.FromMilliseconds(3000)
            };

            using (var rabbitConnection = connectionFactory.CreateConnection())
            {
                using (var channel = rabbitConnection.CreateModel())
                {
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