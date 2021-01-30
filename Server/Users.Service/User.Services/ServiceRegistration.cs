using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using User.Services.Contracts.ServiceTypes;

namespace User.Services
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddDomainServices();
            return services;
        }

        private static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            var transientServiceInterfaceType = typeof(ITransientService);
            var singletonServiceInterfaceType = typeof(ISingletonService);
            var scopedServiceInterfaceType = typeof(IScopedService);

            var types = Assembly
                .GetExecutingAssembly()
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
