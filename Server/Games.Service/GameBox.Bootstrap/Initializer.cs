using GameBox.Application.Contracts.Services;
using GameBox.Persistence;
using Message.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace GameBox.Bootstrap
{
    public static class Initializer
    {
        public static IHost Initialize(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var database = services.GetRequiredService<GameDbContext>();
                var messages = services.GetRequiredService<MessageDbContext>();
                var serviceBus = services.GetRequiredService<IQueueSenderService>();

                database.Database.Migrate();
                messages.Database.Migrate();

                Task.Run(async () => await GameDbContextSeed.SeedDatabaseAsync(database, serviceBus))
                    .GetAwaiter()
                    .GetResult();
            }

            return host;
        }
    }
}
