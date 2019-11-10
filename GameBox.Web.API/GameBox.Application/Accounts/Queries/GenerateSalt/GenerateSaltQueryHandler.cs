using MediatR;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Queries.GenerateSalt
{
    public class GenerateSaltQueryHandler : IRequestHandler<GenerateSaltQuery, byte[]>
    {
        public async Task<byte[]> Handle(GenerateSaltQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    byte[] salt = new byte[128 / 8];
                    rng.GetBytes(salt);

                    return salt;
                }
            });
        }
    }
}
