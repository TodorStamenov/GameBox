using System;
using System.Text.Json.Serialization;

namespace GameBox.Admin.UI.Model
{
    public class UserModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("expirationDate")]
        public DateTime ExpirationDate { get; set; } = DateTime.Now;

        [JsonPropertyName("isAdmin")]
        public bool IsAdmin { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}