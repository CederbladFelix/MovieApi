using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.Models.DTOs;
using Movies.Services.Contracts;
using Swashbuckle.AspNetCore.Annotations;


namespace Movies.Presentation.Controllers
{
    [Route("api/movies/{movieId}/actors")]
    [ApiController]
    [Produces("application/json")]
    public class ActorsController(IServiceManager _serviceManager) : ControllerBase
    {
        private readonly IServiceManager _serviceManager = _serviceManager;


        [HttpPost]
        [SwaggerOperation(Summary = "Connect actor to movie", Description = "Connect actor to a movie.", Tags = ["Movie", "Actor"])]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieActorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieActorDto>> AddActorToMovie(int movieId, [FromBody] MovieActorCreateWithActorIdDto dto)
        {
            var movieActorDto = await _serviceManager.ActorService.AddActorToMovieAsync(movieId, dto);

            return Created(
                $"/api/movies/{movieId}/actors/{dto.ActorId}",
                movieActorDto
            );
        }

        [HttpPost("with-actor/{actorId}")]
        [SwaggerOperation(Summary = "Connect actor to movie", Description = "Connect actor to a movie.", Tags = ["Movie", "Actor"])]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieActorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieActorDto>> AddActorToMovie(int movieId, int actorId, [FromBody] MovieActorCreateDto dto)
        {
            var movieActorDto = await _serviceManager.ActorService.AddActorToMovieAsync(movieId, actorId, dto);

            return Created(
                $"/api/movies/{movieId}/actors/{actorId}",
                movieActorDto
            );
        }
    }
}
