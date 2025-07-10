namespace Movies.Core.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public Genre Genre { get; set; } = null!;
        public int GenreId { get; set; }
        public int Duration { get; set; }

        public MovieDetails MovieDetails { get; set; } = null!;
        public ICollection<Review> Reviews { get; set;} = new List<Review>();
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
