using GameBox.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GameBox.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GameBoxDbContext>(
                options => options.UseSqlServer(
                    configuration["ConnectionString"],
                    sqlOptions => 
                    {
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                        sqlOptions.MigrationsAssembly(typeof(GameBoxDbContext).Assembly.FullName);
                    }));

            services.AddScoped<IGameBoxDbContext>(provider => provider.GetService<GameBoxDbContext>());

            return services;
        }
    }
}
