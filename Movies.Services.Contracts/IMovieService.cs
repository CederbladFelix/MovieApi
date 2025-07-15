using Movies.Core.Models.DTOs;
using System.Threading.Tasks;

namespace Movies.Services.Contracts
{
    public interface IMovieService
    {
        Task<PagedResultDto<MovieDto>> GetMoviesAsync(PaginationOptionsDto paginationOptions, bool includeGenre = false);
        Task<MovieDto> GetMovieAsync(int id, bool includeGenre = false);
        Task<MovieDetailDto> GetMovieDetailsAsync(int id, MovieQueryOptionsDto options);
        Task PutMovieAsync(int id, MovieUpdateDto dto);
        Task<MovieDto> PostMovieAsync(MovieCreateDto dto);
        Task DeleteMovieAsync(int id);
    }
}
