using GameBox.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBox.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MinLength(Constants.UserConstants.UsernameMinLength)]
        [MaxLength(Constants.UserConstants.UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [MinLength(Constants.UserConstants.PasswordMinLength)]
        [MaxLength(Constants.UserConstants.PasswordMaxLength)]
        public string Password { get; set; }

        public bool IsLocked { get; set; }

        public byte[] Salt { get; set; }

        public List<UserRoles> Roles { get; set; } = new List<UserRoles>();
    }
}