using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Comments.Queries.GetCommentsByGame;

public class GetCommentsByGameQuery : IRequest<IEnumerable<CommentsListViewModel>>
{
    public Guid GameId { get; set; }

    public class GetCommentsByGameQueryHandler : IRequestHandler<GetCommentsByGameQuery, IEnumerable<CommentsListViewModel>>
    {
        private readonly IMapper mapper;
        private readonly IDataService context;

        public GetCommentsByGameQueryHandler(IMapper mapper, IDataService context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<CommentsListViewModel>> Handle(GetCommentsByGameQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(this.context
                .All<Comment>()
                .Where(c => c.GameId == request.GameId)
                .OrderByDescending(c => c.TimeStamp)
                .ProjectTo<CommentsListViewModel>(this.mapper.ConfigurationProvider)
                .ToList());
        }
    }
}
