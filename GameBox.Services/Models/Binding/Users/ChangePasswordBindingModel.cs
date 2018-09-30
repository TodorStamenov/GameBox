using GameBox.Core;
using System.ComponentModel.DataAnnotations;

namespace GameBox.Services.Models.Binding.Users
{
    public class ChangePasswordBindingModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(
            Constants.UserConstants.PasswordMaxLength,
            MinimumLength = Constants.UserConstants.PasswordMinLength)]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword))]
        public string RepeatPassword { get; set; }
    }
}