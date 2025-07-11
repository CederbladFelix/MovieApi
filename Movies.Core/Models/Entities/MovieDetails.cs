using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Models.Entities
{
    public class MovieDetails : Entity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Synopsis { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Language { get; set; } = null!;

        [Range(0, 1_000_000_000)]
        public int Budget { get; set; }

        public Movie Movie { get; set; } = null!;
        public int MovieId { get; set; }
    }
}
