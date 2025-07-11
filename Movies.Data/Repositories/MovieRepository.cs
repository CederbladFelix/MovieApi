using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.DTOs;
using Movies.Core.Models.Entities;
using Movies.Data.Data;

namespace Movies.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {

        public MovieRepository(MovieApiContext movieApiContext) : base(movieApiContext) { }

        public async Task<IEnumerable<Movie>> GetMoviesIncludingGenre()
        {
            return await Db.Include(m => m.Genre).ToListAsync();
        }

        public async Task<Movie?> GetMovieIncludingGenre(int id)
        {
            return await Db.Where(m => m.Id == id).Include(m => m.Genre).FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<Movie>> GetMoviesWithQueryOptionsAsync(MovieQueryOptions options)
        {
            IQueryable<Movie> query = Db;

            if (options.IncludeGenre)
                query = query.Include(m => m.Genre);

            if (options.IncludeDetails)
                query = query.Include(m => m.MovieDetails);

            if (options.IncludeReviews)
                query = query.Include(m => m.Reviews);

            if (options.IncludeActors)
                query = query.Include(m => m.MovieActors)
                             .ThenInclude(m => m.Actor);

            return await query.ToListAsync();
        }

        public async Task<Movie?> GetMovieWithQueryOptionsAsync(int id, MovieQueryOptions options)
        {
            IQueryable<Movie> query = Db.Where(m => m.Id == id);

            if (options.IncludeGenre)
                query = query.Include(m => m.Genre);

            if (options.IncludeDetails)
                query = query.Include(m => m.MovieDetails);

            if (options.IncludeReviews)
                query = query.Include(m => m.Reviews);

            if (options.IncludeActors)
                query = query.Include(m => m.MovieActors)
                             .ThenInclude(m => m.Actor);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Genre> GetGenreByNameAsync(string genreName)
        {
            return await Context.Set<Genre>()
                .FirstAsync(g => g.Name == genreName);
        }

        public async Task<bool> AnyMovieAsync(int id)
        {
            return await Db.AnyAsync(m => m.Id == id);
        }
    }
}
