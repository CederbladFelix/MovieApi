using Bogus;
using Bogus.DataSets;
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
                var assignedActors = faker.PickRandom(actors, faker.Random.Int(0, actors.Count))
                                          .ToList();
                var assignedReviews = faker.PickRandom(GenerateReviews(10), 5)
                                          .ToList();

                var movie = new Movie
                {
                    Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Commerce.ProductName()),
                    Year = faker.Date.Past(20).Year,
                    Genre = faker.PickRandom(genres),
                    Duration = faker.Random.Int(60, 180),

                    MovieDetails = new MovieDetails
                    {
                        Synopsis = faker.Lorem.Sentence(20),
                        Language = faker.PickRandom(new[] { "English", "Swedish", "French", "German", "Japanese" }),
                        Budget = faker.Random.Int(20000, 1000000)
                    },

                    Reviews = assignedReviews,

                };

                movie.MovieActors = assignedActors.Select(actor => new MovieActor
                {
                    Movie = movie,
                    Actor = actor,
                    Role = faker.Name.FirstName()
                })
                .ToList();

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
            return Enumerable.Range(1, numberOfActors)
                .Select(_ => new Actor
                {
                    Name = faker.Name.FullName(),
                    BirthYear = faker.Date.Past(80, DateTime.Today.AddYears(-18)).Year
                })
                .ToList();

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
