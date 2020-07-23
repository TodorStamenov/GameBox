using AutoMapper;

namespace GameBox.Application.Contracts.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}