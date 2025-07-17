using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Models.DTOs.Validated
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
