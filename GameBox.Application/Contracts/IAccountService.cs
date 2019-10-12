namespace GameBox.Application.Contracts
{
    public interface IAccountService
    {
        string HashPassword(string password, byte[] salt);

        byte[] GenerateSalt();

        string GenerateJwtToken(string username, bool isAdmin = false);
    }
}
