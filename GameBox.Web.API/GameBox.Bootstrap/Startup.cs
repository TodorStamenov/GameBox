using System.Reflection;
using FluentValidation.AspNetCore;
using GameBox.Api.Filters;
using GameBox.Application;
using GameBox.Application.Categories.Commands.CreateCategory;
using GameBox.Infrastructure;
using GameBox.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameBox.Bootstrap
{
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
                .AddApplication(Assembly.GetAssembly(typeof(AccountService)), Assembly.GetAssembly(typeof(DataService)))
                .AddPersistence(this.Configuration)
                .AddInfrastructure(this.Configuration)
                .AddCors()
                .AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>())
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseRouting()
                .UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());

            // app.UseGraphQL<GameBoxSchema>("/api/graphql");
            // app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}