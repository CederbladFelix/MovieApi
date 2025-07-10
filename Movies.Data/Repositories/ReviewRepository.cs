using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.Entities;
using Movies.Data.Data;

namespace Movies.Data.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {

        public ReviewRepository(MovieApiContext movieApiContext) : base(movieApiContext) { }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await Db.ToListAsync();
        }

        public async Task<Review?> GetAsync(int id)
        {
            return await Db.FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<bool> AnyAsync(int id)
        {
            return await Db.AnyAsync(r => r.Id == id);
        }
    }
}
