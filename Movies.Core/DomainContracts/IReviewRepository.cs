using Movies.Core.Models.DTOs;
using Movies.Core.Models.Entities;

namespace Movies.Core.DomainContracts
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsAsync();
        Task<Review?> GetReviewAsync(int id);
        Task<IEnumerable<Review>> GetReviewsForMovieAsync(int movieId, PaginationOptionsDto paginationOptions);
        Task<bool> AnyReviewAsync(int id);
        Task<int> ReviewCountForMovieAsync(int movieId);
    }
}