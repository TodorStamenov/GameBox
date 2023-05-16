using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Message.DataAccess;

public static class ServiceRegistration
{
    public static IServiceCollection AddMessagePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MessageDbContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString("Messages"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(MessageDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));

        return services;
    }
}
