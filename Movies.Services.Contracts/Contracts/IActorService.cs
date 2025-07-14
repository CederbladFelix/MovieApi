using Movies.Core.Models.DTOs;

namespace Movies.Services.Contracts.Contracts
{
    public interface IActorService
    {
        Task<MovieActorDto> AddActorToMovieAsync(int movieId, MovieActorCreateWithActorIdDto dto);
        Task<MovieActorDto> AddActorToMovieAsync(int movieId, int actorId, MovieActorCreateDto dto);
    }
}
