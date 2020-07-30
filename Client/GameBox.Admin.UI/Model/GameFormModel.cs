using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameBox.Admin.UI.Model
{
    public class GameFormModel
    {
        [Required(ErrorMessage = "Title is required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 symbols long!")]
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Description is required!")]
        [StringLength(int.MaxValue, MinimumLength = 20, ErrorMessage = "Description must be at least 3 symbols long!")]
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be more than 0.01!")]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Size must be more than 0.01!")]
        [JsonPropertyName("size")]
        public double Size { get; set; }
        
        [JsonPropertyName("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
        
        [Required(ErrorMessage = "Video Id is required!")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Title must 11 symbols long!")]
        [JsonPropertyName("videoId")]
        public string VideoId { get; set; }
        
        [JsonPropertyName("releaseDate")]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "CategoryId is required!")]
        [JsonPropertyName("categoryId")]
        public string CategoryId { get; set; }
    }
}