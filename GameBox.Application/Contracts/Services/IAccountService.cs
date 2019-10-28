using System;

namespace GameBox.Application.Contracts.Services
{
    public interface IAccountService
    {
        byte[] GenerateSalt();

        string HashPassword(string password, byte[] salt);

        string GenerateJwtToken(string username, bool isAdmin = false);
    }
}
