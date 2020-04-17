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
                .AddApplication()
                .AddPersistence(this.Configuration)
                .AddInfrastructure();

            services
                .AddCors()
                .AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>())
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            // app.UseGraphQL<GameBoxSchema>("/api/graphql");
            // app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}