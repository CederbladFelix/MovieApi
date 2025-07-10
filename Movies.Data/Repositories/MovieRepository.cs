using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.Entities;
using Movies.Data.Data;

namespace Movies.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {

        public MovieRepository(MovieApiContext movieApiContext) : base(movieApiContext) { }

        public async Task<IEnumerable<Movie>> GetAllAsync(bool includeGenre = false)
        {
            return includeGenre ? await Db.Include(m => m.Genre).ToListAsync() :
                                  await Db.ToListAsync();
        }

        public async Task<Movie?> GetAsync(int id, bool includeGenre = false)
        {
            return includeGenre ? await Db.Where(m => m.Id == id).Include(m => m.Genre).FirstOrDefaultAsync() :
                                  await Db.FirstOrDefaultAsync();
        }
        public async Task<bool> AnyAsync(int id)
        {
            return await Db.AnyAsync(m => m.Id == id);
        }
    }
}
