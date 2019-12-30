using GameBox.Application.Infrastructure;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Accounts.Queries.GenerateJwtToken
{
    public class GenerateJwtTokenQueryHandler : IRequestHandler<GenerateJwtTokenQuery, string>
    {
        public async Task<string> Handle(GenerateJwtTokenQuery request, CancellationToken cancellationToken)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Common.SymmetricSecurityKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim("UserId", request.Id)
            };

            if (request.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, Constants.Common.Admin));
            }

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Constants.Common.TokenExpiration),
                signingCredentials: signinCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
    }
}
