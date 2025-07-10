using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.Entities;
using Movies.Data.Data;

namespace Movies.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {

        public MovieRepository(MovieApiContext movieApiContext) : base(movieApiContext) { }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await Db.ToListAsync();
        }

        public async Task<Movie?> GetAsync(int id)
        {
            return await Db.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<bool> AnyAsync(int id)
        {
            return await Db.AnyAsync(m => m.Id == id);
        }
    }
}
