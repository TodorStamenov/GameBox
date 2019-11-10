using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Queries.HashPassword
{
    public class HashPasswordQueryHandler : IRequestHandler<HashPasswordQuery, string>
    {
        public async Task<string> Handle(HashPasswordQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: request.Password,
                    salt: request.Salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8)));
        }
    }
}
