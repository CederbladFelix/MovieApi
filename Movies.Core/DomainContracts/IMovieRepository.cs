using Movies.Core.Models.DTOs;
using Movies.Core.Models.Entities;

namespace Movies.Core.DomainContracts
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetMoviesAsync(PaginationOptionsDto paginationOptions, bool includeGenre = false);
        Task<Movie?> GetMovieAsync(int id, bool includeGenre = false);
        Task<IEnumerable<Movie>> GetMoviesWithQueryOptionsAsync(MovieQueryOptionsDto options);
        Task<Movie?> GetMovieWithQueryOptionsAsync(int id, MovieQueryOptionsDto options);
        Task<bool> AnyMovieAsync(int id);
        Task<Genre> GetGenreByNameAsync(string genreName);

    }
}
