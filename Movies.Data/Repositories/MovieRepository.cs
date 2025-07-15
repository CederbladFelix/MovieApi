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
        
        public async Task<IEnumerable<Movie>> GetMoviesAsync(PaginationOptionsDto paginationOptions, bool includeGenre = false)
        {
            return includeGenre ? await FindAll()
                                            .Include(m => m.Genre)
                                            .OrderBy(m => m.Id)
                                            .Skip((paginationOptions.CurrentPage - 1) * paginationOptions.PageSize)
                                            .Take(paginationOptions.PageSize)
                                            .ToListAsync() : 

                                  await FindAll()
                                            .OrderBy(p => p.Id)
                                            .Skip((paginationOptions.CurrentPage - 1) * paginationOptions.PageSize)
                                            .Take(paginationOptions.PageSize)
                                            .ToListAsync();
        }

        public async Task<Movie?> GetMovieAsync(int id, bool includeGenre = false)
        {
            return includeGenre ? await FindByCondition(m => m.Id == id).Include(m => m.Genre).FirstOrDefaultAsync() :
                                  await FindByCondition(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesWithQueryOptionsAsync(MovieQueryOptionsDto options)
        {
            IQueryable<Movie> query = FindAll();

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

        public async Task<Movie?> GetMovieWithQueryOptionsAsync(int id, MovieQueryOptionsDto options)
        {
            IQueryable<Movie> query = FindByCondition(m => m.Id == id);

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

        public async Task<bool> AnyMovieAsync(int id)
        {
            return await FindAll().AnyAsync(m => m.Id == id);
        }

        public async Task<int> MovieCountAsync()
        {
            return await FindAll().CountAsync();
        }

        public async Task<Genre> GetGenreByNameAsync(string genreName)
        {
            return await Context.Set<Genre>()
                .FirstAsync(g => g.Name == genreName);
        }
    }
}
