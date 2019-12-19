using GameBox.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace GameBox.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                Task.Run(async () => 
                    {
                        var mediator = scope
                            .ServiceProvider
                            .GetService<IMediator>();

                        await scope
                            .ServiceProvider
                            .GetService<IGameBoxDbContext>()
                            .SeedAsync(mediator);
                    })
                    .GetAwaiter()
                    .GetResult();
            }

            host.Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder => webBuilder.UseStartup<Startup>());
    }
}