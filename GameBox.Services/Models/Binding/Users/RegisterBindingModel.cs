using GameBox.Core;
using System.ComponentModel.DataAnnotations;

namespace GameBox.Services.Models.Binding.Users
{
    public class RegisterBindingModel
    {
        [Required]
        [StringLength(
            Constants.UserConstants.UsernameMaxLength,
            MinimumLength = Constants.UserConstants.UsernameMinLength)]
        public string Username { get; set; }

        [Required]
        [StringLength(
            Constants.UserConstants.PasswordMaxLength,
            MinimumLength = Constants.UserConstants.PasswordMinLength)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string RepeatPassword { get; set; }
    }
}