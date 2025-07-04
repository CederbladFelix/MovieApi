using MovieApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class MovieCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [Range(1950, 2050)]
        public int Year { get; set; }

        [Required]
        [ValidGenre]
        public string Genre { get; set; } = null!;

        [Range(60, 180)]
        public int Duration { get; set; }

        [Required]
        [StringLength(1000)]
        public string Synopsis { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Language { get; set; } = null!;

        [Range(0, 1_000_000_000)]
        public int Budget { get; set; }
    }

}
