using GameBox.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameBox.Services.Models.Binding.Games
{
    public class GameBindingModel
    {
        [Required]
        [MinLength(Constants.GameConstants.TitleMinLength)]
        [MaxLength(Constants.GameConstants.TitleMaxLength)]
        public string Title { get; set; }

        [Range(
            Constants.GameConstants.MinPrice,
            Constants.GameConstants.MaxPrice)]
        public decimal Price { get; set; }

        [Range(
            Constants.GameConstants.MinSize,
            Constants.GameConstants.MaxSize)]
        public double Size { get; set; }

        [Required]
        [MinLength(Constants.GameConstants.MinVideoIdLength)]
        [MaxLength(Constants.GameConstants.MaxVideoIdLength)]
        public string VideoId { get; set; }

        public string ThumbnailUrl { get; set; }

        [Required]
        [MinLength(Constants.GameConstants.MinDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string ReleaseDate { get; set; }

        public Guid CategoryId { get; set; }
    }
}