using Movies.Core.Models.DTOs;
using Movies.Core.Models.Entities;

namespace Movies.Core.DomainContracts
{
    public interface IMovieRepository : IRepository<Movie>, IInternalRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetMoviesAsync(bool includeGenre = false);
        Task<Movie?> GetMovieAsync(int id, bool includeGenre = false);
        Task<IEnumerable<Movie>> GetMoviesWithQueryOptionsAsync(MovieQueryOptions options);
        Task<Movie?> GetMovieWithQueryOptionsAsync(int id, MovieQueryOptions options);
        Task<bool> AnyMovieAsync(int id);
        Task<Genre> GetGenreByNameAsync(string genreName);

    }
}
