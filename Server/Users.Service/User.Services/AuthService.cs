using Message.DataAccess;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using User.DataAccess;
using User.Services.BindingModels.Auth;
using User.Services.Contracts;
using User.Services.Exceptions;
using User.Services.Infrastructure;
using User.Services.Messages;
using User.Services.ViewModels.Auth;

namespace User.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserDbContext userDatabase;
        private readonly MessageDbContext messageDatabase;
        private readonly IQueueSenderService queueSender;

        public AuthService(
            UserDbContext userDatabase,
            MessageDbContext messageDatabase,
            IQueueSenderService queueSender)
        {
            this.userDatabase = userDatabase;
            this.messageDatabase = messageDatabase;
            this.queueSender = queueSender;
        }

        public async Task<LoginViewModel> LoginAsync(LoginBindingModel model)
        {
            var userInfo = await this.userDatabase
                .Users
                .Where(u => u.Username == model.Username)
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Password,
                    u.Salt,
                    u.IsLocked,
                    IsAdmin = u.Roles.Any(r => r.Role.Name == Constants.Common.Admin)
                })
                .FirstOrDefaultAsync();

            if (userInfo == null)
            {
                throw new InvalidCredentialsException();
            }

            if (userInfo.IsLocked)
            {
                throw new AccountLockedException(userInfo.Username);
            }

            var hashedPassword = this.HashPassword(model.Password, userInfo.Salt);

            if (userInfo.Password != hashedPassword)
            {
                throw new InvalidCredentialsException();
            }

            var token = this.GenerateJwtToken(userInfo.Id.ToString(), userInfo.Username, userInfo.IsAdmin);

            return new LoginViewModel
            {
                Id = userInfo.Id,
                Username = userInfo.Username,
                IsAdmin = userInfo.IsAdmin,
                Token = token,
                ExpirationDate = DateTime.UtcNow.AddDays(Constants.Common.TokenExpiration),
                Message = string.Format(Constants.Common.Success, nameof(User), userInfo.Username, "Logged In")
            };
        }

        public async Task<RegisterViewModel> RegisterAsync(RegisterBindingModel model)
        {
            var salt = this.GenerateSalt();
            var hashedPassword = this.HashPassword(model.Password, salt);

            var user = new Models.User
            {
                Username = model.Username,
                Password = hashedPassword,
                Salt = salt
            };

            await this.userDatabase.Users.AddAsync(user);
            await this.userDatabase.SaveChangesAsync();

            var queueName = "users";
            var messageData = new UserRegisteredMessage
            {
                UserId = user.Id,
                Username = user.Username
            };

            var message = new Message.DataAccess.Models.Message(queueName, messageData);

            await this.messageDatabase.Messages.AddAsync(message);
            await this.messageDatabase.SaveChangesAsync();

            this.queueSender.PostQueueMessage(queueName, messageData);

            message.MarkAsPublished();
            await this.messageDatabase.SaveChangesAsync();

            var token = this.GenerateJwtToken(user.Id.ToString(), user.Username, false);

            return new RegisterViewModel
            {
                Username = user.Username,
                Token = token,
                IsAdmin = false,
                ExpirationDate = DateTime.UtcNow.AddDays(Constants.Common.TokenExpiration),
                Message = string.Format(Constants.Common.Success, nameof(User), user.Username, "Registered")
            };
        }

        public async Task<ChangePasswordViewModel> ChangePasswordAsync(ChangePasswordBindingModel model)
        {
            var user = await this.userDatabase
                .Users
                .Where(u => u.Username == model.Username)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException(nameof(User), model.Username);
            }

            var oldHashedPassword = this.HashPassword(model.OldPassword, user.Salt);

            if (oldHashedPassword != user.Password)
            {
                throw new InvalidCredentialsException();
            }

            user.Salt = this.GenerateSalt();

            var newHashedPassword = this.HashPassword(model.NewPassword, user.Salt);

            user.Password = newHashedPassword;

            await this.userDatabase.SaveChangesAsync();

            return new ChangePasswordViewModel { Message = "You have successfully updated your password!" };
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
                issuer: "http://localhost:5001",
                audience: "http://localhost:5001",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Constants.Common.TokenExpiration),
                signingCredentials: signinCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
    }
}
