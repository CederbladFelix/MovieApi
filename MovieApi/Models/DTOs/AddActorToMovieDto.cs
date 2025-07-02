using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class AddActorToMovieDto
    {
        [Required]
        [StringLength(25, MinimumLength = 5)]
        public string CharacterName { get; set; } = null!;
    }
}
