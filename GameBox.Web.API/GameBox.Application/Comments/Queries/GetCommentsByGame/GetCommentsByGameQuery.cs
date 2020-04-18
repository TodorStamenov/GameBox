using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Comments.Queries.GetCommentsByGame
{
    public class GetCommentsByGameQuery : IRequest<IEnumerable<CommentsListViewModel>>
    {
        public Guid GameId { get; set; }

        public class GetCommentsByGameQueryHandler : IRequestHandler<GetCommentsByGameQuery, IEnumerable<CommentsListViewModel>>
        {
            private readonly IMapper mapper;
            private readonly IGameBoxDbContext context;

            public GetCommentsByGameQueryHandler(IMapper mapper, IGameBoxDbContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<IEnumerable<CommentsListViewModel>> Handle(GetCommentsByGameQuery request, CancellationToken cancellationToken)
            {
                return await this.context
                    .Comments
                    .Where(c => c.GameId == request.GameId)
                    .OrderByDescending(c => c.TimeStamp)
                    .ProjectTo<CommentsListViewModel>(this.mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}