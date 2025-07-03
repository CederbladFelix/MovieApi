using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;
using MovieApi.Models.Entities;

namespace MovieApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Produces("application/json")]
    public class MoviesController(MovieApiContext context) : ControllerBase
    {
        private readonly MovieApiContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var moviesDto = await _context.Movies
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    Genre = m.Genre.Name,
                    Duration = m.Duration
                })
                .ToListAsync();

            return Ok(moviesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movieDto = await _context.Movies
                .Where(m => m.Id == id)
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    Genre = m.Genre.Name, 
                    Duration = m.Duration
                })
                .FirstOrDefaultAsync();

            if (movieDto == null) 
                return NotFound();

            return Ok(movieDto);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
        {
            var movieDetailDto = await _context.Movies
                .Where(m => m.Id == id)
                .Select(m => new MovieDetailDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    Genre = m.Genre.Name,
                    Duration = m.Duration,
                    Synopsis = m.MovieDetails.Synopsis,
                    Language = m.MovieDetails.Language,
                    Budget = m.MovieDetails.Budget,

                    Reviews = m.Reviews
                        .Select(r => new ReviewDto
                        {
                            Id = r.Id,
                            ReviewerName = r.ReviewerName,
                            Comment = r.Comment,
                            Rating = r.Rating
                        })
                        .ToList(),

                    Actors = m.MovieActors
                        .Select(ma => new ActorDto
                        {
                            Id = ma.Actor.Id,
                            Name = ma.Actor.Name,
                            BirthYear = ma.Actor.BirthYear
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();


            if (movieDetailDto == null) 
                return NotFound();

            return Ok(movieDetailDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) 
                return NotFound();

            var genre = await _context.Genres
                .FirstOrDefaultAsync(g => g.Name.ToLower() == dto.Genre.Trim().ToLower());

            if (genre == null)
                return BadRequest();

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Genre = genre;
            movie.Duration = dto.Duration;

            movie.MovieDetails.Synopsis = dto.Synopsis;
            movie.MovieDetails.Language = dto.Language;
            movie.MovieDetails.Budget = dto.Budget;
            

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
        public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto dto)
        {
            var genre = await _context.Genres
                .FirstOrDefaultAsync(g => g.Name.ToLower() == dto.Genre.Trim().ToLower());

            if (genre == null)
                return BadRequest();

            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Genre = genre,
                Duration = dto.Duration,

                MovieDetails = new MovieDetails
                {
                    Synopsis = dto.Synopsis,
                    Language = dto.Language,
                    Budget = dto.Budget
                }
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = genre.Name,
                Duration = movie.Duration
            };

            return CreatedAtAction(nameof(GetMovie), new { id = movieDto.Id }, movieDto);
        }

        [HttpDelete("{id}")]
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
