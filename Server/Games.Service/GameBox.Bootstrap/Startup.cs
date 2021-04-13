using FluentValidation.AspNetCore;
using GameBox.Api.Filters;
using GameBox.Application;
using GameBox.Application.Categories.Commands.CreateCategory;
using GameBox.Infrastructure;
using GameBox.Infrastructure.Settings;
using GameBox.Persistence;
using Message.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
                .AddApplication(
                    this.Configuration,
                    Assembly.GetAssembly(typeof(DateTimeService)),
                    Assembly.GetAssembly(typeof(DataService)))
                .AddPersistence(this.Configuration)
                .AddMessagePersistence(this.Configuration)
                .AddInfrastructure()
                .Configure<RabbitMQSettings>(this.Configuration.GetSection("RabbitMQ"))
                .AddCors()
                .AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>())
                .AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Game.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game.Api v1"));
            }

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