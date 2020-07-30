using Blazored.LocalStorage;
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
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IHttpClientService, HttpClientService>();

            builder.Services.AddScoped(sp => new ConfigurationSettings
            {
                BaseAppUrl = builder.Configuration["BaseAppUrl"],
                OrdersApiUrl = builder.Configuration["OrdersApiUrl"],
                GameBoxApiUrl = builder.Configuration["GameBoxApiUrl"]
            });

            builder.Services.AddScoped(sp => new HttpClient());

            await builder.Build().RunAsync();
        }
    }
}
