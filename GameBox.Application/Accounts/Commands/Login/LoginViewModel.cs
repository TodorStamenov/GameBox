namespace GameBox.Application.Accounts.Commands.Login
{
    public class LoginViewModel
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public bool IsAdmin { get; set; }

        public string Message { get; set; }
    }
}
