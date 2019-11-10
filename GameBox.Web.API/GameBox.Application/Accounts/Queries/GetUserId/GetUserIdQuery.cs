using MediatR;
using System;

namespace GameBox.Application.Accounts.Queries.GetUserId
{
    public class GetUserIdQuery : IRequest<Guid>
    {
        public string Username { get; set; }
    }
}
