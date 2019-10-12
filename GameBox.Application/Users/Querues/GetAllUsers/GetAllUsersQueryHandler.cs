using AutoMapper;
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
                query = query
                    .Where(u => u.Username.ToLower().Contains(request.QueryString.ToLower()));
            }

            var users = await query
                .Include(u => u.Roles)
                .ThenInclude(u => u.Role)
                .OrderBy(u => u.Username)
                .Take(UsersOnPage)
                .ToListAsync(cancellationToken);

            return this.mapper.Map<IEnumerable<UsersListViewModel>>(users);
        }
    }
}
