using AutoMapper;

namespace GameBox.Application.Contracts.Mapping
{
    public interface ICustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}