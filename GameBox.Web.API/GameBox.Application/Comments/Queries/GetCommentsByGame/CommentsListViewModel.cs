using AutoMapper;
using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;
using System;

namespace GameBox.Application.Comments.Queries.GetCommentsByGame
{
    public class CommentsListViewModel : IHaveCustomMapping
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Content { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Username { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration
                .CreateMap<Comment, CommentsListViewModel>()
                .ForMember(cfg => cfg.Username, opt => opt.MapFrom(src => src.User.Username));
        }
    }
}