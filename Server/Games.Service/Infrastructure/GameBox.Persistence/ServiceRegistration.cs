using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameBox.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();
        services.AddDbContext<GameDbContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString("Games"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(GameDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));

        return services;
    }
}
