using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.DTOs;
using Movies.Core.Models.Entities;
using Movies.Data.Data;

namespace Movies.Data.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {

        public ReviewRepository(MovieApiContext movieApiContext) : base(movieApiContext) { }

        public async Task<IEnumerable<Review>> GetReviewsAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Review?> GetReviewAsync(int id)
        {
            return await FindAll().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> GetReviewsForMovieAsync(int movieId, PaginationOptionsDto paginationOptions)
        {
            return await FindByCondition(r => r.MovieId == movieId)
                            .OrderBy(r => r.Id)
                            .Skip((paginationOptions.Page - 1) * paginationOptions.PageSize)
                            .Take(paginationOptions.PageSize)
                            .ToListAsync();
        }

        public async Task<bool> AnyReviewAsync(int id)
        {
            return await FindAll().AnyAsync(r => r.Id == id);
        }
    }
}
