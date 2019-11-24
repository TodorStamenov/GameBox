using GameBox.Application.Contracts.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace GameBox.Infrastructure
{
    public class MessageQueueSenderService : IMessageQueueSenderService
    {
        public void Send<T>(string queueName, T command) where T : class
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "192.168.99.100",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
                RequestedConnectionTimeout = 3000
            };

            using (var rabbitConnection = connectionFactory.CreateConnection())
            {
                using (var channel = rabbitConnection.CreateModel())
                {
                    var message = JsonConvert.SerializeObject(command);
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
