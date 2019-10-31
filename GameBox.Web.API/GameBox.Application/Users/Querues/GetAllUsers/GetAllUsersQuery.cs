using MediatR;
using System.Collections.Generic;

namespace GameBox.Application.Users.Querues.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UsersListViewModel>>
    {
        public string QueryString { get; set; }
    }
}
