using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;
using System;

namespace GameBox.Application.Comments.Queries.GetComment
{
    public class CommentViewModel : IMapFrom<Comment>
    {
        public Guid UserId { get; set; }

        public string Content { get; set; }
    }
}