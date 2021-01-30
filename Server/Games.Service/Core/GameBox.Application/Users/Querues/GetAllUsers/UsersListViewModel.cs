using AutoMapper;
using GameBox.Application.Contracts.Mapping;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using System;
using System.Linq;

namespace GameBox.Application.Users.Querues.GetAllUsers
{
    public class UsersListViewModel : ICustomMapping
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public bool IsLocked { get; set; }

        public bool IsAdmin { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration
                .CreateMap<User, UsersListViewModel>()
                .ForMember(
                    cfg => cfg.IsAdmin,
                    opt => opt.MapFrom(src => src.Roles.Any(r => r.Role.Name == Constants.Common.Admin)));
        }
    }
}
