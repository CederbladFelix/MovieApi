using Movies.Core.Models.DTOs;
using Movies.Core.Models.Entities;

namespace Movies.Core.DomainContracts
{
    public interface IMovieRepository
    {
        Task<Movie?> GetAsync(int id);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetMovieWithQueryOptionsAsync(int id, MovieQueryOptions options);
        Task<IEnumerable<Movie>> GetMoviesWithQueryOptionsAsync(MovieQueryOptions options);
        Task<Genre> GetGenreByNameAsync(string genreName);
        Task<bool> AnyAsync(int id);
        void Add(Movie movie);
        void Delete(Movie movie);
        void Update(Movie movie);

    }
}
