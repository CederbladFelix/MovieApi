using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;

namespace MovieApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ReviewsController(MovieApiContext context, IMapper mapper) : ControllerBase
    {
        private readonly MovieApiContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet("api/movies/{movieId}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview(int movieId)
        {
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);
            
            if (!movieExists) 
                return NotFound();


            var reviews = await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .ToListAsync();

            var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews);

            return Ok(reviewsDto);
        }
    }
}
