using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;
using MovieApi.Models.Entities;

namespace MovieApi.Controllers
{
    [Route("api/movies/{movieId}/actors")]
    [ApiController]
    [Produces("application/json")]
    public class ActorsController(MovieApiContext context) : ControllerBase
    {
        private readonly MovieApiContext _context = context;

        [HttpPost()]
        public async Task<ActionResult<MovieActorDto>> AddActorToMovie(int movieId, [FromBody] MovieActorCreateWithActorIdDto dto)
        {
            var actorExists = await _context.Actors.AnyAsync(a => a.Id == dto.ActorId);
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);

            if (!actorExists || !movieExists)
                return NotFound();

            var actorAlreadyInMovie = await _context.MovieActors
                .AnyAsync(ma => ma.ActorId == dto.ActorId && ma.MovieId == movieId);

            if (actorAlreadyInMovie)
                return Conflict();

            var movieActor = new MovieActor
            {
                MovieId = movieId,
                ActorId = dto.ActorId,
                Role = dto.Role
            };

            _context.MovieActors.Add(movieActor);
            await _context.SaveChangesAsync();

            var movieActorDto = new MovieActorDto
            {
                MovieId = movieActor.MovieId,
                ActorId = movieActor.ActorId,
                Role = movieActor.Role
            };

            return Created(
                $"/api/movies/{movieId}/actors/{dto.ActorId}",
                movieActorDto
            );

        }

        [HttpPost("with-actor/{actorId}")]
        public async Task<ActionResult<MovieActorDto>> AddActorToMovie(int movieId, int actorId, [FromBody] MovieActorCreateDto dto)
        {
            var actorExists = await _context.Actors.AnyAsync(a => a.Id == actorId);
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);

            if (!actorExists || !movieExists)
                return NotFound();

            var actorAlreadyInMovie = await _context.MovieActors
                .AnyAsync(ma => ma.ActorId == actorId && ma.MovieId == movieId);

            if (actorAlreadyInMovie)
                return Conflict();

            var movieActor = new MovieActor
            {
                MovieId = movieId,
                ActorId = actorId,
                Role = dto.Role
            };

            _context.MovieActors.Add(movieActor);
            await _context.SaveChangesAsync();

            var movieActorDto = new MovieActorDto
            {
                MovieId = movieActor.MovieId,
                ActorId = movieActor.ActorId,
                Role = movieActor.Role
            };

            return Created(
                $"/api/movies/{movieId}/actors/{actorId}",
                movieActorDto
            );
        }
    }
}
