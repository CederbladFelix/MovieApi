using Movies.Core.Models.Entities;

namespace Movies.Core.DomainContracts
{
    public interface IMovieRepository
    {
        Task<bool> AnyAsync(int id);
        Task<Movie?> GetAsync(int id);
        Task<IEnumerable<Movie>> GetAllAsync();
    }
}
