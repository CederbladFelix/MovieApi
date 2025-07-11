using Movies.Core.Models.Entities;

namespace Movies.Core.DomainContracts
{
    public interface IActorRepository
    {
        Task<bool> AnyAsync(int id);
        Task<Actor?> GetAsync(int id);
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<bool> InMovieAsync(int id, int movieId);
        void AddMovieActor(MovieActor movieActor);
    }
}