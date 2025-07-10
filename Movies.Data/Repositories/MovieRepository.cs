using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.Entities;
using Movies.Data.Data;

namespace Movies.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly MovieApiContext movieApiContext;

        public MovieRepository(MovieApiContext movieApiContext)
        {
            this.movieApiContext = movieApiContext;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await movieApiContext.Movies.ToListAsync();
        }

        public async Task<Movie> GetAsync(int id)
        {
            return await movieApiContext.Movies.FirstAsync(m => m.Id == id);
        }
        public async Task<bool> AnyAsync(int id)
        {
            return await movieApiContext.Movies.AnyAsync(m => m.Id == id);
        }

        public void Add(Movie movie)
        {
            movieApiContext.Movies.Add(movie);
        }

        public void Delete(Movie movie)
        {
            movieApiContext.Movies.Remove(movie);
        }

        public void Update(Movie movie)
        {
            movieApiContext.Movies.Update(movie);
        }
    }
}
