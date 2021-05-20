using GameBox.Persistence;
using Message.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

                database.Database.Migrate();
                messages.Database.Migrate();
            }

            return host;
        }
    }
}
