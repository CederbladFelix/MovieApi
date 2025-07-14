namespace Movies.Core.Models.DTOs
{
    public class MovieQueryOptionsDto
    {
        public bool IncludeGenre { get; set; } = false;
        public bool IncludeDetails { get; set; } = false;
        public bool IncludeReviews { get; set; } = false;
        public bool IncludeActors { get; set; } = false;
    }
}
