using Movies.Core.Models.Entities;

namespace Movies.Core.DomainContracts
{
    public interface IReviewRepository
    {
        Task<bool> AnyAsync(int id);
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetAsync(int id);
        Task<IEnumerable<Review>> GetAllForMovieAsync(int movieId);
    }
}