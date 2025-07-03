using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class ActorDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public int BirthYear { get; set; }
    }

}
