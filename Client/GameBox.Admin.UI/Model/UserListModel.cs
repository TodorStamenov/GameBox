using System.Text.Json.Serialization;

namespace GameBox.Admin.UI.Model
{
    public class UserListModel
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("isLocked")]
        public bool IsLocked { get; set; }

        [JsonPropertyName("isAdmin")]
        public bool IsAdmin { get; set; }
    }
}