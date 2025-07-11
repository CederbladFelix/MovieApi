using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.DTOs;
using Movies.Core.Models.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.Api.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Produces("application/json")]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MovieQueryOptions _includeAll;

        public MoviesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _includeAll = new MovieQueryOptions
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
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync();
            var moviesDto = _mapper.Map<IEnumerable<MovieDto>>(movies);

            return Ok(moviesDto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get movie by ID", Description = "Returns a specified movie.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDetailDto))]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {          
            var movie = await _unitOfWork.Movies.GetAsync(id);
            if (movie == null)
                return NotFound();

            var movieDto = _mapper.Map<MovieDto>(movie);

            return Ok(movieDto);
        }

        [HttpGet("{id}/details")]
        [SwaggerOperation(Summary = "Get moviedetails by ID", Description = "Returns full details of a movie.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDetailDto))]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
        {
            var movie = await _unitOfWork.Movies.GetMovieWithQueryOptionsAsync(id, options: _includeAll);

            if (movie == null)
                return NotFound();

            var movieDto = _mapper.Map<MovieDetailDto>(movie);

            return Ok(movieDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update movie", Description = "Updates an existing movie by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
        {
            var movie = await _unitOfWork.Movies.GetAsync(id);

            if (movie == null)
                return NotFound();

            var genre = await _unitOfWork.Movies.GetGenreByNameAsync(dto.Genre);

            _mapper.Map(dto, movie);
            movie.Genre = genre;

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MovieExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create movie", Description = "Creates a new movie.", Tags = ["Movie"])]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto dto)
        {
            var genre = await _unitOfWork.Movies.GetGenreByNameAsync(dto.Genre);

            var movie = _mapper.Map<Movie>(dto);
            movie.Genre = genre;

            _unitOfWork.Movies.Add(movie);
            await _unitOfWork.CompleteAsync();

            var movieDto = _mapper.Map<MovieDto>(movie);

            return CreatedAtAction(nameof(GetMovie), new { id = movieDto.Id }, movieDto);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete movie", Description = "Deletes a movie by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _unitOfWork.Movies.GetAsync(id);

            if (movie == null)
                return NotFound();

            _unitOfWork.Movies.Delete(movie);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        private async Task<bool> MovieExists(int id)
        {
            return await _unitOfWork.Movies.AnyAsync(id);
        }
    }
}
