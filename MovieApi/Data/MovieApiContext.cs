using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Configurations;
using MovieApi.Models.Entities;

namespace MovieApi.Data
{
    public class MovieApiContext : DbContext
    {
        public MovieApiContext (DbContextOptions<MovieApiContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<Actor> Actors { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<MovieActor> MovieActors { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MovieConfigurations());
            modelBuilder.ApplyConfiguration(new MovieActorConfigurations());

        }
    }
}
