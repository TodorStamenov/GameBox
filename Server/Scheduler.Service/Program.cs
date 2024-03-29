﻿using Scheduler.Service.Contracts;
using Scheduler.Service.Model;
using Scheduler.Service.Services;

namespace Scheduler.Service;

public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(context => context
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build())
            .ConfigureServices((context, services) => services
                .AddOptions()
                .AddLogging()
                .AddStackExchangeRedisCache(options => options.Configuration = context.Configuration.GetConnectionString("Redis"))
                .Configure<RabbitMQSettings>(context.Configuration.GetSection("RabbitMQ"))
                .AddSingleton<IQueueSenderService, QueueSenderService>()
                .AddHostedService<BrokerHostedService>()
                .AddHostedService<RedisCacheHostedService>());
    }
}
