﻿using AutoMapper;
using FluentValidation.AspNetCore;
using GameBox.Api.Filters;
using GameBox.Application.Categories.Commands.CreateCategory;
using GameBox.Application.Contracts;
using GameBox.Application.Contracts.Services;
using GameBox.Application.GraphQL;
using GameBox.Application.Infrastructure;
using GameBox.Application.Infrastructure.AutoMapper;
using GameBox.Application.Infrastructure.Extensions;
using GameBox.Infrastructure;
using GameBox.Persistence;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace GameBox.Api
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
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddGraphQLServices();

            services.AddMediatR(typeof(CreateCategoryCommandValidator).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddSingleton<IMessageQueueSenderService, MessageQueueSenderService>();

            // services.AddDomainServices(typeof(MessageQueueSenderService).Assembly);

            var connString = "Server=tcp:192.168.99.100,5433;Initial Catalog=GameBoxCore;User Id=sa;Password=Your_password@123";

            services.AddDbContext<IGameBoxDbContext, GameBoxDbContext>(options =>
                options.UseSqlServer(connString));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://localhost:5000",
                        ValidAudience = "http://localhost:5000",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Common.SymmetricSecurityKey))
                    });

            services.AddCors(options =>
                options.AddPolicy("EnableCORS", builder =>
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build()));

            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors("EnableCORS");
            app.UseGraphQL<GameBoxSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseMvc();
        }
    }
}