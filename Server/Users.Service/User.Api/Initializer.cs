using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using User.DataAccess;
using User.Services.Contracts;

namespace User.Api
{
    public static class Initializer
    {
        public static IHost Initialize(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var database = services.GetRequiredService<UserDbContext>();
                var account = services.GetRequiredService<IAuthService>();

                database.Database.Migrate();

                Task.Run(async () => await UserDbContextSeed.SeedDatabaseAsync(database, account.GenerateSalt, account.HashPassword))
                    .GetAwaiter()
                    .GetResult();
            }

            return host;
        }
    }
}