using Bogus;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;
using System.Globalization;

namespace MovieApi.Data
{
    public class SeedData
    {
        private static Faker faker = new();
        internal static async Task InitAsync(MovieApiContext context)
        {
            if (await context.Movies.AnyAsync()) return;

            var genres = GenerateGenres();
            await context.AddRangeAsync(genres);
            await context.SaveChangesAsync();

            var actors = GenerateActors(10);
            await context.AddRangeAsync(actors);
            await context.SaveChangesAsync();

            var movies = GenerateMovies(10, actors, genres);
            await context.AddRangeAsync(movies);
            await context.SaveChangesAsync();
        }


        private static IEnumerable<Movie> GenerateMovies(int numberOfMovies, List<Actor> actors, List<Genre> genres)
        {
            var movies = new List<Movie>(numberOfMovies);

            for (int i = 0; i < numberOfMovies; i++)
            {
                var assignedActors = faker.PickRandom(actors, faker.Random.Int(0, actors.Count)).ToList();


                var assignedReviews = faker.PickRandom(GenerateReviews(10), 5).ToList();

                var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Commerce.ProductName());
                var year = faker.Date.Past(20).Year;
                var genre = faker.PickRandom(genres);

                var duration = faker.Random.Int(60, 180);

                var movie = new Movie
                {
                    Title = title,
                    Year = year,
                    GenreId = genre.Id,
                    Genre = genre,
                    Duration = duration,

                    MovieDetails = new MovieDetails
                    {
                        Synopsis = faker.Lorem.Sentence(20),
                        Language = faker.PickRandom(new[] { "English", "Swedish", "French", "German", "Japanese" }),
                        Budget = faker.Random.Int(20000, 1000000)
                    },

                    Reviews = assignedReviews,

                    MovieActors = assignedActors.Select(actor => new MovieActor
                    {
                        ActorId = actor.Id,
                        Actor = actor,
                        Role = faker.Name.FirstName()
                    })
                    .ToList()
                };

                movies.Add(movie);
            }
            return movies;
        }
        private static List<Genre> GenerateGenres()
        {
            var genreNames = new[] 
            {   
                "Action", 
                "Drama", 
                "Comedy", 
                "Horror", 
                "Documentary", 
                "Fantasy" 
            };

            return genreNames.Select(name => new Genre 
            { 
                Name = name 
            })
            .ToList();
        }


        private static List<Actor> GenerateActors(int numberOfActors)
        {
            var actors = new List<Actor>();

            for (int i = 0; i < numberOfActors; i++)
            {
                var name = faker.Name.FullName();
                var birthYear = faker.Date.Past(80, DateTime.Today.AddYears(-18)).Year;
                var actor = new Actor
                {
                    Name = name,
                    BirthYear = birthYear
                };
                actors.Add(actor);
                
            }

            return actors;
        }

        private static List<Review> GenerateReviews(int numberOfReviews)
        {
            var reviews = new List<Review>();

            for (int i = 0; i < numberOfReviews; i++)
            {
                var reviewerName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Name.FullName());
                var comment = $"A truly {faker.Hacker.Adjective()} movie.";
                var rating = faker.Random.Int(1, 5);
                var review = new Review
                {
                    ReviewerName = reviewerName,
                    Comment = comment,
                    Rating = rating
                };
                reviews.Add(review);

            }

            return reviews;
        }
    }
}
