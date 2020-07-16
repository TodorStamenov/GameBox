﻿using GameBox.Scheduler.Contracts;
using GameBox.Scheduler.Model;
using GameBox.Scheduler.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace GameBox.Scheduler
{
    public class Program
    {
        public static async Task Main()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration(context =>
                {
                    context
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddOptions();
                    services.AddSingleton<IQueueSenderService, QueueSenderService>();
                    services.AddHostedService<SchedulerHostedService>();
                    services.Configure<Settings>(options => {
                        options.ConnectionString = context.Configuration.GetValue<string>("ConnectionString");
                        options.RabbitMQHost = context.Configuration.GetValue<string>("RabbitMQ:Host");
                        options.RabbitMQPort = context.Configuration.GetValue<int>("RabbitMQ:Port");
                        options.RabbitMQUsername = context.Configuration.GetValue<string>("RabbitMQ:Username");
                        options.RabbitMQPassword = context.Configuration.GetValue<string>("RabbitMQ:Password");
                    });
                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}
