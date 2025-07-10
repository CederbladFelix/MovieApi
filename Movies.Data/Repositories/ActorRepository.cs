using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.Entities;
using Movies.Data.Data;

namespace Movies.Data.Repositories
{
    public class ActorRepository : Repository<Actor>, IActorRepository
    {

        public ActorRepository(MovieApiContext movieApiContext) : base(movieApiContext) { }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await Db.ToListAsync();
        }

        public async Task<Actor?> GetAsync(int id)
        {
            return await Db.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<bool> AnyAsync(int id)
        {
            return await Db.AnyAsync(a => a.Id == id);
        }
    }
}
