namespace GameBox.Application.Contracts.Services
{
    public interface IAccountService
    {
        string HashPassword(string password, byte[] salt);

        byte[] GenerateSalt();

        string GenerateJwtToken(string username, bool isAdmin = false);
    }
}
