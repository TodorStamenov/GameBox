using GameBox.Scheduler.Contracts;
using GameBox.Scheduler.Model;
using RabbitMQ.Client;
using System;
using System.Text;

namespace GameBox.Scheduler.Services
{
    public class QueueSenderService : IQueueSenderService
    {
        private readonly Settings settings;
        
        public QueueSenderService(Settings settings)
        {
            this.settings = settings;
        }

        public void PostQueueMessage(string queueName, string message)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = this.settings.RabbitMQHost,
                UserName = this.settings.RabbitMQUsername,
                Password = this.settings.RabbitMQPassword,
                Port = this.settings.RabbitMQPort,
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