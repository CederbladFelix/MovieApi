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
        [SwaggerOperation(Summary = "Get reviews for a movie with pagination", Description = "Gets all reviews for a movie with pagination.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultDto<ReviewDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedResultDto<ReviewDto>>> GetReviewsForMovie(int movieId, [FromQuery] PaginationOptionsDto paginationOptions)
        {
            return Ok(await _serviceManager.ReviewService.GetReviewsForMovieAsync(movieId, paginationOptions));

        }
    }
}
