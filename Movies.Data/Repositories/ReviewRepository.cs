using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.Entities;
using Movies.Data.Data;

namespace Movies.Data.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {

        public ReviewRepository(MovieApiContext movieApiContext) : base(movieApiContext) { }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await Db.ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllReviewsForMovieAsync(int movieId)
        {
            return await Db
                .Where(r => r.MovieId == movieId)
                .ToListAsync();
        }

        public async Task<Review?> GetReviewAsync(int id)
        {
            return await Db.FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<bool> AnyReviewAsync(int id)
        {
            return await Db.AnyAsync(r => r.Id == id);
        }
    }
}
