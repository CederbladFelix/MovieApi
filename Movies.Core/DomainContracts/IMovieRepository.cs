using Movies.Core.Models.Entities;

namespace Movies.Core.DomainContracts
{
    public interface IMovieRepository
    {
        Task<bool> AnyAsync(int id);
        Task<Movie?> GetAsync(int id, bool includeGenre = false);
        Task<IEnumerable<Movie>> GetAllAsync(bool includeGenre = false);
    }
}
