namespace Movies.Core.Models.Entities
{
    public class Genre : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
