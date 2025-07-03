using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public string Genre { get; set; } = null!;
        public int Duration { get; set; }
        public string Synopsis { get; set; } = null!;
        public string Language { get; set; } = null!;
        public int Budget { get; set; }
        public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
        public ICollection<ActorDto> Actors { get; set; } = new List<ActorDto>();
    }
}
