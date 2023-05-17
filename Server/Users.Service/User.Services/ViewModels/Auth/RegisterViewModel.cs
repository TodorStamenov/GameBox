namespace User.Services.ViewModels.Auth;

public class RegisterViewModel
{
    public string Username { get; set; }

    public string Token { get; set; }

    public bool IsAdmin { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string Message { get; set; }
}
