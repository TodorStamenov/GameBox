using System;

namespace GameBox.Admin.UI.Model
{
    public class UserModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsAdmin { get; set; }
    }
}