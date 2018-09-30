using System.ComponentModel.DataAnnotations;

namespace GameBox.Services.Models.Binding.Users
{
    public class LoginBindingModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}