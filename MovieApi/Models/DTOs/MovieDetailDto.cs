using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public int Duration { get; set; }
        public string GenreName { get; set; } = null!;
        public string MovieDetailsSynopsis { get; set; } = null!;
        public string MovieDetailsLanguage { get; set; } = null!;
        public int MovieDetailsBudget { get; set; }
        public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
        public ICollection<ActorDto> Actors { get; set; } = new List<ActorDto>();
    }
}
