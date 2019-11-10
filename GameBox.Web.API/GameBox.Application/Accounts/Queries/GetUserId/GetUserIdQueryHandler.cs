using GameBox.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Queries.GetUserId
{
    public class GetUserIdQueryHandler : IRequestHandler<GetUserIdQuery, Guid>
    {
        private readonly IGameBoxDbContext context;

        public GetUserIdQueryHandler(IGameBoxDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> Handle(GetUserIdQuery request, CancellationToken cancellationToken)
        {
            return await this.context
                .Users
                .Where(u => u.Username == request.Username)
                .Select(u => u.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
