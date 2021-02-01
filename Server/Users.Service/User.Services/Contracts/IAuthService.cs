using User.Services.Contracts.ServiceTypes;

namespace User.Services.Contracts
{
    public interface IAuthService : IScopedService
    {
        byte[] GenerateSalt();

        string HashPassword(string password, byte[] salt);

        string GenerateJwtToken(string id, string username, bool isAdmin);
    }
}
