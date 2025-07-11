using Movies.Core.Models.Entities;

namespace Movies.Core.DomainContracts
{
    public interface IActorRepository
    {
        Task<bool> AnyActorAsync(int id);
        Task<Actor?> GetActorAsync(int id);
        Task<IEnumerable<Actor>> GetAllActorsAsync();
        Task<bool> InMovieAsync(int id, int movieId);
        void AddMovieActor(MovieActor movieActor);
    }
}