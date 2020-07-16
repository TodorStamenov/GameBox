using GameBox.Scheduler.Contracts;
using GameBox.Scheduler.Model;
using GameBox.Scheduler.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GameBox.Scheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(context =>
                {
                    context
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();
                })
                .ConfigureServices((context, services) =>
                {
                    var configuration = context.Configuration;

                    var settings = new Settings
                    {
                        ConnectionString = configuration.GetValue<string>("ConnectionString"),
                        RabbitMQHost = configuration.GetValue<string>("RabbitMQ:Host"),
                        RabbitMQPort = configuration.GetValue<int>("RabbitMQ:Port"),
                        RabbitMQUsername = configuration.GetValue<string>("RabbitMQ:Username"),
                        RabbitMQPassword = configuration.GetValue<string>("RabbitMQ:Password")
                    };

                    services
                        .AddOptions()
                        .AddLogging()
                        .AddSingleton<Settings>(_ => settings)
                        .AddSingleton<IQueueSenderService, QueueSenderService>()
                        .AddHostedService<SchedulerHostedService>();
                });
        }
    }
}
