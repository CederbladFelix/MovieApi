using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Models.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public int Duration { get; set; }
        public string GenreName { get; set; } = null!;
    }
}
