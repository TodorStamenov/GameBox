using GameBox.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBox.Data.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

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

        public DateTime ReleaseDate { get; set; }

        public int ViewCount { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public List<GameOrder> Orders { get; set; } = new List<GameOrder>();
    }
}