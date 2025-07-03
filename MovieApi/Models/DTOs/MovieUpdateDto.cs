using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class MovieUpdateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = null!;
        [Range(1950, 2050)]
        public int Year { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Genre { get; set; } = null!;
        [Range(60, 180)]
        public int Duration { get; set; }

        [Required]
        public string Synopsis { get; set; } = null!;
        [Required]
        public string Language { get; set; } = null!;
        public int Budget { get; set; }
    }
}