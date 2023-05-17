using Message.DataAccess;
using Microsoft.EntityFrameworkCore;
using User.DataAccess;

namespace User.Api;

public static class Initializer
{
    public static IHost Initialize(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var database = services.GetRequiredService<UserDbContext>();
            var messages = services.GetRequiredService<MessageDbContext>();

            database.Database.Migrate();
            messages.Database.Migrate();
        }

        return host;
    }
}
