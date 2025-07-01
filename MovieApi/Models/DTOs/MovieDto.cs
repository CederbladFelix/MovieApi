using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        [Required]
        public string Genre { get; set; } = null!;
        public int Duration { get; set; }
    }
}
