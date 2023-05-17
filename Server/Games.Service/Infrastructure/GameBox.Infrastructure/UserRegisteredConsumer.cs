using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using GameBox.Infrastructure.Messages;
using GameBox.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace GameBox.Infrastructure;

public class UserRegisteredConsumer : IHostedService
{
    private IModel channel;
    private IConnection connection;
    private readonly IServiceScopeFactory scopeFactory;
    private readonly RabbitMQSettings settings;

    public UserRegisteredConsumer(
        IServiceScopeFactory scopeFactory,
        IOptions<RabbitMQSettings> settings)
    {
        this.scopeFactory = scopeFactory;
        this.settings = settings.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.InitRabbitMQ();
        var consumer = new EventingBasicConsumer(this.channel);

        consumer.Received += async (ch, ea) =>
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var database = scope.ServiceProvider.GetRequiredService<IDataService>();
                var content = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<UserRegisteredMessage>(content);

                var customer = new Customer
                {
                    UserId = message.UserId,
                    Username = message.Username
                };

                await database.AddAsync(customer);
                await database.SaveAsync(cancellationToken);

                this.channel.BasicAck(ea.DeliveryTag, false);
            }
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
