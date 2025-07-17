using Movies.Core.Models.DTOs;
using Movies.Core.Models.DTOs.Validated;

namespace Movies.Services.Contracts
{
    public interface IActorService
    {
        Task<MovieActorDto> AddActorToMovieAsync(int movieId, MovieActorCreateWithActorIdDto dto);
        Task<MovieActorDto> AddActorToMovieAsync(int movieId, int actorId, MovieActorCreateDto dto);
    }
}
