using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.Models.DTOs;
using Movies.Services.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.Presentation.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Produces("application/json")]
    public class MoviesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly MovieQueryOptionsDto _includeAll;

        public MoviesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;

            _includeAll = new MovieQueryOptionsDto
            {
                IncludeActors = true,
                IncludeDetails = true,
                IncludeGenre = true,
                IncludeReviews = true
            };

        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all movies", Description = "Gets all movies.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieDto>))]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies([FromQuery] int pageSize, [FromQuery] PaginationOptionsDto paginationOptions)
        {
            return Ok(await _serviceManager.MovieService.GetMoviesAsync(paginationOptions, includeGenre: true));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get movie by ID", Description = "Returns a specified movie.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDetailDto))]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            return Ok(await _serviceManager.MovieService.GetMovieAsync(id, includeGenre: true));
        }

        [HttpGet("{id}/details")]
        [SwaggerOperation(Summary = "Get moviedetails by ID", Description = "Returns full details of a movie.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDetailDto))]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
        {
            return Ok(await _serviceManager.MovieService.GetMovieDetailsAsync(id, options: _includeAll));
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update movie", Description = "Updates an existing movie by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
        {
            await _serviceManager.MovieService.PutMovieAsync(id, dto);
            return NoContent();       
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create movie", Description = "Creates a new movie.", Tags = ["Movie"])]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto dto)
        {
            var movieDto = await _serviceManager.MovieService.PostMovieAsync(dto);
            return CreatedAtAction(nameof(GetMovie), new { id = movieDto.Id }, movieDto);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete movie", Description = "Deletes a movie by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _serviceManager.MovieService.DeleteMovieAsync(id);
            return NoContent();
        }

    }
}
