using AutoMapper;
using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;
using System;

namespace GameBox.Application.Categories.Queries.GetAllCategories;

public class CategoriesListViewModel : ICustomMapping
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Games { get; set; }

    public void CreateMappings(Profile configuration)
    {
        configuration
            .CreateMap<Category, CategoriesListViewModel>()
            .ForMember(cfg => cfg.Games, opt => opt.MapFrom(src => src.Games.Count));
    }
}
