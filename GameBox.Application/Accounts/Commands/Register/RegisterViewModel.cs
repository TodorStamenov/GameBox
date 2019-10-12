namespace GameBox.Application.Accounts.Commands.Register
{
    public class RegisterViewModel
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public bool IsAdmin { get; set; }

        public string Message { get; set; }
    }
}