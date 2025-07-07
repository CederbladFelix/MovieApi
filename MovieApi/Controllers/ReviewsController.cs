using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ReviewsController(MovieApiContext context, IMapper mapper) : ControllerBase
    {
        private readonly MovieApiContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet("api/movies/{movieId}/reviews")]
        [SwaggerOperation(Summary = "Get reviews for a movie", Description = "Gets all reviews for a movie.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview(int movieId)
        {
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);

            if (!movieExists)
                return NotFound();

            var reviewsDto = await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(reviewsDto);
        }
    }
}
