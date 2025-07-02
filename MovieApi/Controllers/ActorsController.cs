using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;
using MovieApi.Models.Entities;

namespace MovieApi.Controllers
{
    [ApiController]
    public class ActorsController(MovieApiContext context) : ControllerBase
    {
        private readonly MovieApiContext _context = context;

        [HttpPost("api/movies/{movieId}/actors/{actorId}")]
        public async Task<IActionResult> AddActorToMovie(int movieId, int actorId, [FromBody] AddActorToMovieDto dto)
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
                CharacterName = dto.CharacterName
            };

            _context.MovieActors.Add(movieActor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
