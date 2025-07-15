using Movies.Core.Models.DTOs;

namespace Movies.Services.Contracts
{
    public interface IReviewService
    {
        Task<PagedResultDto<ReviewDto>> GetReviewsForMovieAsync(int movieId, PaginationOptionsDto paginationOptions);
    }
}
