using AutoMapper;
using GameBox.Application.Contracts;
using GameBox.Application.GraphQL;
using GameBox.Application.Infrastructure;
using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace GameBox.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }

        public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<ISchema, GameBoxSchema>();

            services
                .AddGraphQL(o => o.ExposeExceptions = true)
                .AddGraphTypes(ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services, Assembly assembly)
        {
            var transientServiceInterfaceType = typeof(ITransientService);
            var singletonServiceInterfaceType = typeof(ISingletonService);
            var scopedServiceInterfaceType = typeof(IScopedService);

            var types = assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.ToLower().EndsWith("service"))
                .Where(t => t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList();

            foreach (var type in types)
            {
                if (transientServiceInterfaceType.IsAssignableFrom(type.Interface))
                {
                    services.AddTransient(type.Interface, type.Implementation);
                }
                else if (singletonServiceInterfaceType.IsAssignableFrom(type.Interface))
                {
                    services.AddSingleton(type.Interface, type.Implementation);
                }
                else if (scopedServiceInterfaceType.IsAssignableFrom(type.Interface))
                {
                    services.AddScoped(type.Interface, type.Implementation);
                }
            }

            return services;
        }
    }
}
