using GameBox.Application.Contracts;
using GameBox.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GameBox.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<IGameBoxDbContext>();
                var concreteContext = (GameBoxDbContext)context;

                GameBoxDbInitializer.SeedDatabase(concreteContext);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}