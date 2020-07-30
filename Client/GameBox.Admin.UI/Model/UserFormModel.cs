using System.ComponentModel.DataAnnotations;

namespace GameBox.Admin.UI.Model
{
    public class UserFormModel
    {
        [Required(ErrorMessage = "Username is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 symbols long!")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Password must be between 3 and 50 symbols long!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat password is required!")]
        [Compare(nameof(Password), ErrorMessage = "Password and repeat password must match!")]
        public string RepeatPassword { get; set; }
    }
}