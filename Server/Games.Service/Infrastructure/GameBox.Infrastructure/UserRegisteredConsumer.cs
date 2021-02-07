using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using GameBox.Infrastructure.Messages;
using GameBox.Infrastructure.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Infrastructure
{
    public class UserRegisteredConsumer : IHostedService
    {
        private IModel channel;
        private IConnection connection;
        private readonly IDataService database;
        private readonly RabbitMQSettings settings;

        public UserRegisteredConsumer(
            IDataService database,
            IOptions<RabbitMQSettings> settings)
        {
            this.database = database;
            this.settings = settings.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.InitRabbitMQ();
            var consumer = new EventingBasicConsumer(this.channel);

            consumer.Received += async (ch, ea) =>
            {
                var content = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<UserRegisteredMessage>(content);

                var customer = new Customer
                {
                    UserId = message.UserId,
                    Username = message.Username
                };

                await this.database.AddAsync(customer);
                await this.database.SaveAsync(cancellationToken);

                this.channel.BasicAck(ea.DeliveryTag, false);
            };

            this.channel.BasicConsume(queue: "users", false, consumer);
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
                queue: "users",
                durable: false,
                exclusive: false,
                autoDelete: true,
                arguments: null);
        }
    }
}
