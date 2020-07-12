using GameBox.Notification.Messages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Notification.Hubs
{
    public class GameCreatedConsumer : IHostedService
    {
        private IModel channel;
        private IConnection connection;
        private readonly IHubContext<NotificationsHub> hub;

        public GameCreatedConsumer(IHubContext<NotificationsHub> hub)
        {
            this.hub = hub;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.InitRabbitMQ();
            var consumer = new EventingBasicConsumer(this.channel);

            consumer.Received += async (ch, ea) =>
            {
                var content = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<GameCreatedMessage>(content);

                await this.hub
                    .Clients
                    .Groups(Constants.AuthenticatedUsersGroup)
                    .SendAsync(Constants.ReceiveNotificationEndpoint, message);

                this.channel.BasicAck(ea.DeliveryTag, false);
            };

            this.channel.BasicConsume("games", false, consumer);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.channel.Close();  
            this.connection.Close();
            return Task.CompletedTask;
        }

        private void InitRabbitMQ()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "172.17.0.1",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
                RequestedConnectionTimeout = TimeSpan.FromMilliseconds(3000)
            };

            this.connection = connectionFactory.CreateConnection();
            this.channel = connection.CreateModel();

            this.channel.QueueDeclare(
                queue: "games",
                durable: false,
                exclusive: false,
                autoDelete: true,
                arguments: null);
        }
    }
}