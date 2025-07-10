using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Models.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ReviewerName { get; set; } = null!;

        [StringLength(500)]
        public string Comment { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
