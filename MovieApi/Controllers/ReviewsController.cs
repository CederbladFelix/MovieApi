using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;

namespace MovieApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ReviewsController(MovieApiContext context) : ControllerBase
    {
        private readonly MovieApiContext _context = context;

        [HttpGet("api/movies/{movieId}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview(int movieId)
        {
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);
            
            if (!movieExists) 
                return NotFound();

            var reviewsDto = await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    ReviewerName = r.ReviewerName,
                    Comment = r.Comment,
                    Rating = r.Rating
                })
            .ToListAsync();

            return Ok(reviewsDto);
        }
    }
}
