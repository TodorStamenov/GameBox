using GameBox.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBox.Data.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MinLength(Constants.RoleConstants.NameMinLength)]
        [MaxLength(Constants.RoleConstants.NameMaxLength)]
        public string Name { get; set; }

        public List<UserRole> Users { get; set; } = new List<UserRole>();
    }
}