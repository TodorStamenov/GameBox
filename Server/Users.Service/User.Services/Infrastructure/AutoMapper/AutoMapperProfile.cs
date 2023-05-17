using AutoMapper;
using System.Reflection;
using User.Services.Contracts.Mapping;

namespace User.Services.Infrastructure.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        var allTypes = Assembly
            .GetExecutingAssembly()
            .DefinedTypes
            .ToList();

        allTypes
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                .Where(i => i.IsGenericType)
                .Select(i => i.GetGenericTypeDefinition())
                .Contains(typeof(IMapFrom<>)))
            .Select(t => new
            {
                Destination = t,
                Source = t
                    .GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Select(i => new
                    {
                        Definition = i.GetGenericTypeDefinition(),
                        Arguments = i.GetGenericArguments()
                    })
                    .Where(i => i.Definition == typeof(IMapFrom<>))
                    .SelectMany(i => i.Arguments)
                    .First(),
            })
            .ToList()
            .ForEach(mapping => this.CreateMap(mapping.Source, mapping.Destination));

        allTypes
            .Where(t => t.IsClass
                && !t.IsAbstract
                && typeof(ICustomMapping).IsAssignableFrom(t))
            .Select(Activator.CreateInstance)
            .Cast<ICustomMapping>()
            .ToList()
            .ForEach(mapping => mapping.CreateMappings(this));
    }
}
