using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;
using MovieApi.Models.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Produces("application/json")]
    public class MoviesController(MovieApiContext context, IMapper mapper) : ControllerBase
    {
        private readonly MovieApiContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [SwaggerOperation(Summary = "Get all movies", Description = "Gets all movies.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieDto>))]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var moviesDto = await _context.Movies
                .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(moviesDto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get movie by ID", Description = "Returns a specified movie.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDetailDto))]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movieDto = await _context.Movies
                .Where(m => m.Id == id)
                .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (movieDto == null)
                return NotFound();

            return Ok(movieDto);
        }

        [HttpGet("{id}/details")]
        [SwaggerOperation(Summary = "Get moviedetails by ID", Description = "Returns full details of a movie.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDetailDto))]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
        {
            var movieDto = await _context.Movies
                .Where(m => m.Id == id)
                .ProjectTo<MovieDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (movieDto == null)
                return NotFound();

            return Ok(movieDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update movie", Description = "Updates an existing movie by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound();

            var genre = await _context.Genres
                .FirstAsync(g => g.Name == dto.Genre.Trim());

            _mapper.Map(dto, movie);
            movie.Genre = genre;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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
            var genre = await _context.Genres
                .FirstAsync(g => g.Name == dto.Genre.Trim());

            var movie = _mapper.Map<Movie>(dto);
            movie.Genre = genre;


            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var movieDto = _mapper.Map<MovieDto>(movie);

            return CreatedAtAction(nameof(GetMovie), new { id = movieDto.Id }, movieDto);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete movie", Description = "Deletes a movie by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
