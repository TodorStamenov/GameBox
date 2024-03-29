using Message.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using User.DataAccess;
using User.Services;
using User.Services.Settings;

namespace User.Api;

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
            .AddPersistence(this.Configuration)
            .AddMessagePersistence(this.Configuration)
            .AddServices()
            .Configure<RabbitMQSettings>(this.Configuration.GetSection("RabbitMQ"))
            .AddCors()
            .AddControllers();

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

            c.SwaggerDoc("v1", new OpenApiInfo { Title = "User.Api", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User.Api v1"));
        }

        app
            .UseRouting()
            .UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<UserDbContextSeedService>();
                endpoints.MapControllers();
            });
    }
}
