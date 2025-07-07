using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;
using MovieApi.Models.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieApi.Controllers
{
    [Route("api/movies/{movieId}/actors")]
    [ApiController]
    [Produces("application/json")]
    public class ActorsController(MovieApiContext context, IMapper mapper) : ControllerBase
    {
        private readonly MovieApiContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [SwaggerOperation(Summary = "Connect actor to movie", Description = "Connect actor to a movie.", Tags = ["Movie", "Actor"])]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieActorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            var movieActor = _mapper.Map<MovieActor>(dto);
            movieActor.MovieId = movieId;

            _context.MovieActors.Add(movieActor);
            await _context.SaveChangesAsync();

            return Created(
                $"/api/movies/{movieId}/actors/{dto.ActorId}",
                _mapper.Map<MovieActorDto>(movieActor)
            );
        }

        [HttpPost("with-actor/{actorId}")]
        [SwaggerOperation(Summary = "Connect actor to movie", Description = "Connect actor to a movie.", Tags = ["Movie", "Actor"])]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieActorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            var movieActor = _mapper.Map<MovieActor>(dto);
            movieActor.MovieId = movieId;
            movieActor.ActorId = actorId;

            _context.MovieActors.Add(movieActor);
            await _context.SaveChangesAsync();

            return Created(
                $"/api/movies/{movieId}/actors/{actorId}",
                _mapper.Map<MovieActorDto>(movieActor)
            );
        }
    }
}
