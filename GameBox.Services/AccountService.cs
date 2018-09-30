using GameBox.Core;
using GameBox.Core.Enums;
using GameBox.Data;
using GameBox.Data.Models;
using GameBox.Services.Contracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GameBox.Services
{
    public class AccountService : Service, IAccountService
    {
        public AccountService(GameBoxDbContext database)
            : base(database)
        {
        }

        public ServiceResult Login(string username, string password)
        {
            var userInfo = Database
                .Users
                .Where(u => u.Username == username)
                .Select(u => new
                {
                    u.Username,
                    u.Password,
                    u.Salt,
                    IsAdmin = u.Roles
                        .Any(r => r.Role.Name == Constants.Common.Admin)
                })
                .FirstOrDefault();

            string hashedPassword = this.HashPassword(password, userInfo?.Salt);

            if (userInfo == null
                || userInfo.Password != hashedPassword)
            {
                return new ServiceResult
                {
                    ResultType = ServiceResultType.Failed,
                    Message = Constants.Common.InvalidCredentials
                };
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Common.SymmetricSecurityKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            if (userInfo.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, Constants.Common.Admin));
            }

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signinCredentials);

            return new ServiceResult
            {
                ResultType = ServiceResultType.Success,
                Message = new JwtSecurityTokenHandler().WriteToken(tokeOptions)
            };
        }

        public ServiceResult Register(string username, string password)
        {
            if (this.HasUser(username))
            {
                return new ServiceResult
                {
                    ResultType = ServiceResultType.Failed,
                    Message = string.Format(Constants.Common.DuplicateEntry, nameof(User), username)
                };
            }

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashedPassword = this.HashPassword(password, salt);

            User user = new User
            {
                Username = username,
                Password = hashedPassword,
                Salt = salt
            };

            Database.Users.Add(user);
            Database.SaveChanges();

            return new ServiceResult
            {
                ResultType = ServiceResultType.Success,
                Message = $"User {username} registered successfully!"
            };
        }

        public ServiceResult ChangePassword(string username, string oldPassword, string newPassword)
        {
            User user = Database
                .Users
                .Where(u => u.Username == username)
                .FirstOrDefault();

            string oldHashedPassword = this.HashPassword(oldPassword, user.Salt);

            if (user == null
                || oldHashedPassword != user.Password)
            {
                return new ServiceResult
                {
                    ResultType = ServiceResultType.Failed,
                    Message = string.Format(Constants.Common.NotExistingEntry, nameof(User), username)
                };
            }

            string newHashedPassword = this.HashPassword(newPassword, user.Salt);

            user.Password = newHashedPassword;

            Database.SaveChanges();

            return new ServiceResult
            {
                ResultType = ServiceResultType.Success,
                Message = string.Format("Password changed successfully!")
            };
        }

        private bool HasUser(string username)
        {
            return Database
                .Users
                .Where(u => u.Username == username)
                .Any();
        }

        private string HashPassword(string password, byte[] salt)
        {
            if (string.IsNullOrEmpty(password)
                || string.IsNullOrWhiteSpace(password)
                || salt == null)
            {
                return null;
            }

            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
        }
    }
}