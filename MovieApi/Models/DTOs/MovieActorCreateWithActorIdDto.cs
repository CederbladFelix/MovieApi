using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class MovieActorCreateWithActorIdDto
    {
        [Required]
        public int ActorId { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 5)]
        public string Role { get; set; } = null!;
    }
}
