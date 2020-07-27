using System.ComponentModel.DataAnnotations;

namespace GameBox.Admin.UI.Model
{
    public class LoginFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username should be at least 3 symbols long!")]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Password should be at least 3 symbols long!")]
        public string Password { get; set; }
    }
}