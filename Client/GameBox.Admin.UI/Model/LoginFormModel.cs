using System.ComponentModel.DataAnnotations;

namespace GameBox.Admin.UI.Model
{
    public class LoginFormModel
    {
        [Required(ErrorMessage = "Username is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be at least 3 symbols long!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Password must be at least 3 symbols long!")]
        public string Password { get; set; }
    }
}