using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameBox.Admin.UI.Model
{
    public class CategoryFormModel
    {
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 symbols long!")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}