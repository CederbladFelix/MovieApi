using AutoMapper;
using Movies.Core.DomainContracts;
using Movies.Core.Models.DTOs;
using Movies.Services.Contracts.Contracts;

namespace Movies.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsForMovieAsync(int movieId)
        {
            var movieExists = await _unitOfWork.Movies.AnyMovieAsync(movieId);

            if (!movieExists)
                return null!;

            var reviews = await _unitOfWork.Reviews.GetReviewsForMovieAsync(movieId);

            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

            return reviewsDto;
        }

    }
}
