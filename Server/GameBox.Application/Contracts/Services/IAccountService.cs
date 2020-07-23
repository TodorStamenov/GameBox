namespace GameBox.Application.Contracts.Services
{
    public interface IAccountService : ITransientService
    {
        byte[] GenerateSalt();

        string HashPassword(string password, byte[] salt);

        string GenerateJwtToken(string id, string username, bool isAdmin);
    }
}
