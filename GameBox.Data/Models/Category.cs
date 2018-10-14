using GameBox.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBox.Data.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MinLength(Constants.CategoryConstants.NameMinLength)]
        [MaxLength(Constants.CategoryConstants.NameMaxLength)]
        public string Name { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}