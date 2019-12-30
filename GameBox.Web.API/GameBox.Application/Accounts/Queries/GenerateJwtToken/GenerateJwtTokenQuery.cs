using MediatR;

namespace GameBox.Application.Accounts.Queries.GenerateJwtToken
{
    public class GenerateJwtTokenQuery : IRequest<string>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public bool IsAdmin { get; set; }
    }
}
