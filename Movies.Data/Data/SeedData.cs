using Bogus;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Models.Entities;
using System.Globalization;

namespace Movies.Data.Data
{
    public class SeedData
    {
        private static readonly Faker _faker = new();
        private static readonly string[] _languages = new[] { "English", "Swedish", "French", "German", "Japanese" };

        public static async Task InitAsync(MovieApiContext context)
        {
            if (await context.Movies.AnyAsync()) return;

            var genres = GenerateGenres();
            await context.AddRangeAsync(genres);

            var actors = GenerateActors(100);
            await context.AddRangeAsync(actors);

            var movies = GenerateMovies(100, actors, genres);
            await context.AddRangeAsync(movies);

            await context.SaveChangesAsync();
        }


        private static IEnumerable<Movie> GenerateMovies(int numberOfMovies, List<Actor> actors, List<Genre> genres)
        {
            var movies = new List<Movie>(numberOfMovies);

            for (int i = 0; i < numberOfMovies; i++)
            {
                var assignedActors = _faker.PickRandom(actors, _faker.Random.Int(0, actors.Count))
                                          .ToList();
                var assignedReviews = _faker.PickRandom(GenerateReviews(10), 5)
                                          .ToList();

                var movie = new Movie
                {
                    Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_faker.Commerce.ProductName()),
                    Year = _faker.Date.Past(20).Year,
                    Genre = _faker.PickRandom(genres),
                    Duration = _faker.Random.Int(60, 180),

                    MovieDetails = new MovieDetails
                    {
                        Synopsis = _faker.Lorem.Sentence(20),
                        Language = _faker.PickRandom(_languages),
                        Budget = _faker.Random.Int(20000, 1000000)
                    },

                    Reviews = assignedReviews,

                };

                movie.MovieActors = assignedActors.Select(actor => new MovieActor
                {
                    Movie = movie,
                    Actor = actor,
                    Role = _faker.Name.FirstName()
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
                    Name = _faker.Name.FullName(),
                    BirthYear = _faker.Date.Past(80, DateTime.Today.AddYears(-18)).Year
                })
                .ToList();

        }

        private static List<Review> GenerateReviews(int numberOfReviews)
        {
            var reviews = new List<Review>();

            for (int i = 0; i < numberOfReviews; i++)
            {
                var reviewerName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_faker.Name.FullName());
                var comment = $"A truly {_faker.Hacker.Adjective()} movie.";
                var rating = _faker.Random.Int(1, 5);
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
