using GameBox.Application.Contracts.Services;
using GameBox.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace GameBox.Api
{
    public static class Initializer
    {
        public static IWebHost Initialize(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var database = services.GetRequiredService<GameBoxDbContext>();
                var serviceBus = services.GetRequiredService<IMessageQueueSenderService>();

                database.Database.Migrate();

                Task.Run(async () => await GameBoxDbContextSeed.SeedDatabaseAsync(database, serviceBus))
                    .GetAwaiter()
                    .GetResult();
            }

            return host;
        }
    }
}
