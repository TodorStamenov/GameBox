﻿using GameBox.Application.Contracts.Services;
using GameBox.Application.Infrastructure;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GameBox.Infrastructure
{
    public class AccountService : IAccountService
    {
        private readonly IDateTimeService dateTime;

        public AccountService(IDateTimeService dateTime)
        {
            this.dateTime = dateTime;
        }

        public byte[] GenerateSalt()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[128 / 8];
                rng.GetBytes(salt);
                return salt;
            }
        }

        public string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
        }

        public string GenerateJwtToken(string id, string username, bool isAdmin)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Common.SymmetricSecurityKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("UserId", id)
            };

            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, Constants.Common.Admin));
            }

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: this.dateTime.UtcNow.AddDays(Constants.Common.TokenExpiration),
                signingCredentials: signinCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
    }
}
