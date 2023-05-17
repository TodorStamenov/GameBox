using AutoMapper;
using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;

namespace GameBox.Application.Categories.Queries.GetMenuCategories;

public class CategoriesListMenuViewModel : ICustomMapping
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool HasGames { get; set; }

    public void CreateMappings(Profile configuration)
    {
        configuration
            .CreateMap<Category, CategoriesListMenuViewModel>()
            .ForMember(cfg => cfg.HasGames, opt => opt.MapFrom(src => src.Games.Count != 0));
    }
}
