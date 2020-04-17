using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameBox.Application.Contracts;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Users.Querues.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UsersListViewModel>>
    {
        public string QueryString { get; set; }

        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UsersListViewModel>>
        {
            private const int UsersOnPage = 15;

            private readonly IMapper mapper;
            private readonly IGameBoxDbContext context;

            public GetAllUsersQueryHandler(IMapper mapper, IGameBoxDbContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<IEnumerable<UsersListViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                IQueryable<User> query = this.context.Users;

                if (!string.IsNullOrWhiteSpace(request.QueryString))
                {
                    query = query.Where(u => u.Username.ToLower().Contains(request.QueryString.ToLower()));
                }

                return await query
                    .OrderBy(u => u.Username)
                    .Take(UsersOnPage)
                    .ProjectTo<UsersListViewModel>(this.mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
