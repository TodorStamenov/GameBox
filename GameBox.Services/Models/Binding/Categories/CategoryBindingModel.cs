using GameBox.Core;
using System.ComponentModel.DataAnnotations;

namespace GameBox.Services.Models.Binding.Categories
{
    public class CategoryBindingModel
    {
        [Required]
        [StringLength(
            Constants.CategoryConstants.NameMaxLength,
            MinimumLength = Constants.CategoryConstants.NameMinLength)]
        public string Name { get; set; }
    }
}