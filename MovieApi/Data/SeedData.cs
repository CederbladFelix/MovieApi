using Bogus;
using Microsoft.CodeAnalysis.Diagnostics;
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

            var actors = GenerateActors(faker.Random.Int(0, 30));
            await context.AddRangeAsync(actors);

            var movies = GenerateMovies(faker.Random.Int(1, 25), actors);
            await context.AddRangeAsync(movies);

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Movie> GenerateMovies(int numberOfMovies, List<Actor> actors)
        {
            var movies = new List<Movie>(numberOfMovies);

            for (int i = 0; i < numberOfMovies; i++)
            {
                int numberOfActors = actors.Count == 0 ? 0 : faker.Random.Int(0, actors.Count);
                var assignedActors = faker.PickRandom(actors, numberOfActors).ToList();

                var assignedReviews = GenerateReviews(faker.Random.Int(0, 15));

                var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Lorem.Sentence(3).Trim('.'));
                var year = faker.Date.Past(20).Year;
                var genre = faker.PickRandom(new[]
                {
                    "Action", "Drama", "Comedy", "Horror", "Documentary", "Fantasy"
                });
                var duration = faker.Random.Int(60, 180);

                var movie = new Movie
                {
                    Title = title,
                    Year = year,
                    Genre = genre,
                    Duration = duration,
                    MovieDetails = new MovieDetails
                    {
                        Synopsis = faker.Lorem.Sentence(20),
                        Language = faker.PickRandom(new[]
                        {
                            "English", "Swedish", "French", "German", "Japanese"
                        }),
                        Budget = faker.Random.Int(20000, 1000000)
                    },
                    Reviews = assignedReviews,
                    Actors = assignedActors
                };
                movies.Add(movie);
            }
            return movies;
        }

        private static List<Actor> GenerateActors(int numberOfActors)
        {
            var actors = new List<Actor>();

            for (int i = 0; i < numberOfActors; i++)
            {
                var name = faker.Name.FullName();
                var birthYear = faker.Person.DateOfBirth.Year;
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
                var comment = $"A truly {faker.Hacker.Adjective} movie.";
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
