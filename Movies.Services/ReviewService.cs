using AutoMapper;
using Movies.Core.DomainContracts;
using Movies.Core.Exceptions;
using Movies.Core.Models.DTOs;
using Movies.Services.Contracts;

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

        public async Task<PagedResultDto<ReviewDto>> GetReviewsForMovieAsync(int movieId, PaginationOptionsDto paginationOptions)
        {
            var movieExists = await _unitOfWork.Movies.AnyMovieAsync(movieId);

            if (!movieExists)
                throw new MovieNotFoundException(movieId);

            var totalItems = await _unitOfWork.Reviews.ReviewCountForMovieAsync(movieId);

            var reviews = await _unitOfWork.Reviews.GetReviewsForMovieAsync(movieId, paginationOptions);

            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

            return new PagedResultDto<ReviewDto>
                (
                    data: reviewsDto,
                    totalItems: totalItems,
                    currentPage: paginationOptions.CurrentPage,
                    pageSize: paginationOptions.PageSize
                );
        }

    }
}
