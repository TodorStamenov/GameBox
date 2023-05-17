using AutoMapper;
using User.Services.Contracts.Mapping;
using User.Services.Infrastructure;

namespace User.Services.ViewModels.Users;

public class UsersListViewModel : ICustomMapping
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public bool IsLocked { get; set; }

    public bool IsAdmin { get; set; }

    public void CreateMappings(Profile configuration)
    {
        configuration
            .CreateMap<Models.User, UsersListViewModel>()
            .ForMember(
                cfg => cfg.IsAdmin,
                opt => opt.MapFrom(src => src.Roles.Any(r => r.Role.Name == Constants.Common.Admin)));
    }
}
