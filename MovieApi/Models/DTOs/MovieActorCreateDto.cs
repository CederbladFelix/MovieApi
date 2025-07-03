using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class MovieActorCreateDto
    {
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Role { get; set; } = null!;
    }
}
