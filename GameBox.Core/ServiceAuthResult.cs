namespace GameBox.Core
{
    public class ServiceAuthResult : ServiceResult
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public bool IsAdmin { get; set; }
    }
}