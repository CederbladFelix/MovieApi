namespace Movies.Core.Models.Entities
{
    public class MovieActor : Entity
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int ActorId { get; set; }
        public Actor Actor { get; set; } = null!;

        public string Role { get; set; } = null!;
    }

}
