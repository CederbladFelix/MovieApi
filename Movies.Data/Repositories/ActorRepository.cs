using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.Entities;
using Movies.Data.Data;

namespace Movies.Data.Repositories
{
    public class ActorRepository : Repository<Actor>, IActorRepository
    {

        public ActorRepository(MovieApiContext movieApiContext) : base(movieApiContext) { }

        public async Task<IEnumerable<Actor>> GetActorsAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Actor?> GetActorAsync(int id)
        {
            return await FindAll().FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<bool> AnyActorAsync(int id)
        {
            return await FindAll().AnyAsync(a => a.Id == id);
        }
        public async Task<bool> ActorInMovieAsync(int id, int movieId)
        {
            return await FindByCondition(a => a.MovieActors.Any(ma => ma.ActorId == id && ma.MovieId == movieId)).AnyAsync();
        }

        public void AddMovieActor(MovieActor movieActor)
        {
            Context.Set<MovieActor>().Add(movieActor);              
        }


    }
}
