using Movies.Core.Models.DTOs;
using System.Threading.Tasks;

namespace Movies.Services.Contracts.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetMoviesAsync(bool includeGenre);
        Task<MovieDto> GetMovieAsync(int id, bool includeGenre = false);
        Task<MovieDetailDto> GetMovieDetailsAsync(int id, MovieQueryOptions options);
        Task<bool> PutMovieAsync(int id, MovieUpdateDto dto);
        Task<MovieDto> PostMovieAsync(MovieCreateDto dto);
        Task<bool> DeleteMovieAsync(int id);
    }
}
