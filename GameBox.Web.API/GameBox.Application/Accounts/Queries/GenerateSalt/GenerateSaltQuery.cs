using MediatR;

namespace GameBox.Application.Accounts.Queries.GenerateSalt
{
    public class GenerateSaltQuery : IRequest<byte[]>
    {
    }
}
