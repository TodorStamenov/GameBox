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
                    u.IsLocked,
                    IsAdmin = u.Roles
                        .Any(r => r.Role.Name == Constants.Common.Admin)
                })
                .FirstOrDefault();

            if (userInfo == null)
            {
                return GetServiceResult(Constants.Common.InvalidCredentials, ServiceResultType.Fail);
            }

            if (userInfo.IsLocked)
            {
                return GetServiceResult($"User {username} is locked!", ServiceResultType.Fail);
            }

            string hashedPassword = this.HashPassword(password, userInfo.Salt);

            if (userInfo.Password != hashedPassword)
            {
                return GetServiceResult(Constants.Common.InvalidCredentials, ServiceResultType.Fail);
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

            return GetServiceResult(new JwtSecurityTokenHandler().WriteToken(tokeOptions), ServiceResultType.Success);
        }

        public ServiceResult Register(string username, string password)
        {
            if (this.HasUser(username))
            {
                return GetServiceResult(string.Format(Constants.Common.DuplicateEntry, nameof(User), username), ServiceResultType.Fail);
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

            return GetServiceResult(string.Format(Constants.Common.Success, nameof(User), username, "Registered"), ServiceResultType.Success);
        }

        public ServiceResult ChangePassword(string username, string oldPassword, string newPassword)
        {
            User user = Database
                .Users
                .Where(u => u.Username == username)
                .FirstOrDefault();

            if (user == null)
            {
                return GetServiceResult(string.Format(Constants.Common.NotExistingEntry, nameof(User), username), ServiceResultType.Fail);
            }

            string oldHashedPassword = this.HashPassword(oldPassword, user.Salt);

            if (oldHashedPassword != user.Password)
            {
                return GetServiceResult("Incorect password!", ServiceResultType.Fail);
            }

            string newHashedPassword = this.HashPassword(newPassword, user.Salt);

            user.Password = newHashedPassword;

            Database.SaveChanges();

            return GetServiceResult("You have successfully updated your password!", ServiceResultType.Success);
        }

        private bool HasUser(string username)
        {
            return Database.Users.Any(u => u.Username == username);
        }

        private string HashPassword(string password, byte[] salt)
        {
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