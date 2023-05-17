using Notification.Service.Hubs;
using Notification.Service.Infrastructure;
using Notification.Service.Models;

namespace Notification.Service;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddCors()
            .AddInfrastructure()
            .Configure<RabbitMQSettings>(this.Configuration.GetSection("RabbitMQ"))
            .AddSignalR();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app
            .UseRouting()
            .UseCors(options => options.WithOrigins("http://172.17.0.1:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials())
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapHub<NotificationsHub>("/notifications"));
    }
}
