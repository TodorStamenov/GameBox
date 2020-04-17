using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Comments.Queries.GetComment
{
    public class GetCommentQuery : IRequest<CommentViewModel>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public bool IsAdmin { get; set; }

        public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, CommentViewModel>
        {
            private readonly IMapper mapper;
            private readonly IGameBoxDbContext context;

            public GetCommentQueryHandler(IMapper mapper, IGameBoxDbContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<CommentViewModel> Handle(GetCommentQuery request, CancellationToken cancellationToken)
            {
                var comment = await this.context
                    .Comments
                    .Where(c => c.Id == request.Id)
                    .ProjectTo<CommentViewModel>(this.mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

                if (comment == null)
                {
                    throw new NotFoundException(nameof(Comment), request.Id);
                }

                if (comment.UserId != request.UserId && !request.IsAdmin)
                {
                    throw new NoAccessException();
                }

                return comment;
            }
        }
    }
}