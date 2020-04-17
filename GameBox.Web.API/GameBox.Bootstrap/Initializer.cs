using GameBox.Application.Contracts.Services;
using GameBox.Persistence;
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

                var database = services.GetRequiredService<GameBoxDbContext>();
                var account = services.GetRequiredService<IAccountService>();
                var serviceBus = services.GetRequiredService<IMessageQueueSenderService>();

                database.Database.Migrate();

                Task.Run(async () => await GameBoxDbContextSeed.SeedDatabaseAsync(database, account, serviceBus))
                    .GetAwaiter()
                    .GetResult();
            }

            return host;
        }
    }
}
