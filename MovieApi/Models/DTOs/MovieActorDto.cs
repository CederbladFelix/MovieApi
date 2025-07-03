namespace MovieApi.Models.DTOs
{
    public class MovieActorDto
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string Role { get; set; } = null!;
    }
}
