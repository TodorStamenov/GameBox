using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Notification.Service.Hubs;
using Notification.Service.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.Service.Messages
{
    public class GameCreatedConsumer : IHostedService
    {
        private IModel channel;
        private IConnection connection;
        private readonly RabbitMQSettings settings;
        private readonly IHubContext<NotificationsHub> hub;

        public GameCreatedConsumer(
            IOptions<RabbitMQSettings> settings,
            IHubContext<NotificationsHub> hub)
        {
            this.settings = settings.Value;
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

            this.channel.BasicConsume(queue: "games", false, consumer);
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
                Port = this.settings.Port,
                HostName = this.settings.Host,
                UserName = this.settings.Username,
                Password = this.settings.Password,
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