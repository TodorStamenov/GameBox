﻿using GameBox.Application.GraphQL;
using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace GameBox.Application.Infrastructure.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, Assembly assembly)
        {
            assembly
                .DefinedTypes
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"))
                .Where(t => t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Type = t
                })
                .ToList()
                .ForEach(t => services.AddTransient(t.Interface, t.Type));

            return services;
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
    }
}
