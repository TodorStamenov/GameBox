using Blazored.Toast;
using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameBox.Admin.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazoredToast();

            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped(_ => new ConfigurationSettings
            {
                BaseAppUrl = builder.HostEnvironment.BaseAddress,
                OrdersApiUrl = builder.Configuration["OrdersApiUrl"],
                GameBoxApiUrl = builder.Configuration["GameBoxApiUrl"]
            });

            builder.Services.AddHttpClient<IHttpClientService, HttpClientService>();

            await builder.Build().RunAsync();
        }
    }
}
