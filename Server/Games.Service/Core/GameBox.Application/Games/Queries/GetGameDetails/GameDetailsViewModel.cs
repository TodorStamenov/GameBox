using AutoMapper;
using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;

namespace GameBox.Application.Games.Queries.GetGameDetails;

public class GameDetailsViewModel : ICustomMapping
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string VideoId { get; set; }

    public string ThumbnailUrl { get; set; }

    public decimal Price { get; set; }

    public double Size { get; set; }

    public int ViewCount { get; set; }

    public string ReleaseDate { get; set; }

    public void CreateMappings(Profile configuration)
    {
        configuration
            .CreateMap<Game, GameDetailsViewModel>()
            .ForMember(cfg => cfg.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate.ToShortDateString()));
    }
}
