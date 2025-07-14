using Movies.Core.Models.DTOs;

namespace Movies.Services.Contracts.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetReviewsForMovieAsync(int movieId);
    }
}
