﻿using FluentValidation.AspNetCore;
using GameBox.Api.Filters;
using GameBox.Application;
using GameBox.Infrastructure;
using GameBox.Infrastructure.Settings;
using GameBox.Persistence;
using Message.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace GameBox.Bootstrap;

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
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
            .AddNewtonsoftJson();

        services.AddSwaggerGen(c =>
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {securityScheme, new string[] { }}
            });

            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Game.Api", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            appBuilder.UseDeveloperExceptionPage();
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game.Api v1"));
        }

        appBuilder
            .UseRouting()
            .UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapControllers();
                endpoints.MapGrpcService<GameDbContextSeedService>();
            });
    }
}
