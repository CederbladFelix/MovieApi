using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.DTOs;
using Movies.Data.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ReviewsController(IUnitOfWork unitOfWork, /*MovieApiContext context,*/ IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        //private readonly MovieApiContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet("api/movies/{movieId}/reviews")]
        [SwaggerOperation(Summary = "Get reviews for a movie", Description = "Gets all reviews for a movie.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForMovie(int movieId)
        {
            var movieExists = await _unitOfWork.Movies.AnyAsync(movieId);

            if (!movieExists)
                return NotFound();

            var reviews = await _unitOfWork.Reviews.GetAllForMovieAsync(movieId);

            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

            /*var reviewsDto = await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .ToListAsync();*/

            return Ok(reviewsDto);
        }
    }
}
