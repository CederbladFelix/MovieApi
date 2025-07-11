using Movies.Core.Models.Entities;

namespace Movies.Core.DomainContracts
{
    public interface IReviewRepository
    {
        Task<bool> AnyReviewAsync(int id);
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review?> GetReviewAsync(int id);
        Task<IEnumerable<Review>> GetAllReviewsForMovieAsync(int movieId);
    }
}