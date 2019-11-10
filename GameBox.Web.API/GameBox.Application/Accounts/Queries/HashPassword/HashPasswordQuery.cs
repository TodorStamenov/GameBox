using MediatR;

namespace GameBox.Application.Accounts.Queries.HashPassword
{
    public class HashPasswordQuery : IRequest<string>
    {
        public string Password { get; set; }

        public byte[] Salt { get; set; }
    }
}
