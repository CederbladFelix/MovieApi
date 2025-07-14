using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.Models.DTOs;
using Movies.Services.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.Presentation.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ReviewsController(IServiceManager serviceManager) : ControllerBase
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        [HttpGet("api/movies/{movieId}/reviews")]
        [SwaggerOperation(Summary = "Get reviews for a movie", Description = "Gets all reviews for a movie.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForMovie(int movieId)
        {
            var reviewDtos = await _serviceManager.ReviewService.GetReviewsForMovieAsync(movieId);

            if (reviewDtos == null)
                return NotFound(); 

            return Ok(reviewDtos);

        }
    }
}
