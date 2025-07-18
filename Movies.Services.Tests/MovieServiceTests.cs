using AutoMapper;
using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using Movies.Core.DomainContracts;
using Movies.Core.Models.DTOs;
using Movies.Core.Models.Entities;
using Movies.Data.Data;
using System.Globalization;

namespace Movies.Services.Tests 
{
    public class MovieServiceTests
    {
        private Mock<IUnitOfWork> _uowMock;
        private MovieService sut;
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>(), new LoggerFactory()));

        public MovieServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();

            sut = new MovieService(_uowMock.Object, _mapper);
        }

        [Fact]
        public async Task GetMoviesAsync_Returns_Expected_PagedResult()
        {
            //Arrange
            const int expectedMovies = 5;
            _uowMock.Setup(x => x.Movies.MovieCountAsync()).ReturnsAsync(10);
            _uowMock.Setup(x => x.Movies.GetMoviesAsync(It.IsAny<PaginationOptionsDto>(), true)).ReturnsAsync(GenerateMovies(expectedMovies));

            //Act
            var resultDto = await sut.GetMoviesAsync(new PaginationOptionsDto { CurrentPage = 1, PageSize = 10 }, true);

            //Assert
            Assert.NotNull(resultDto);
            Assert.IsType<PagedResultDto<MovieDto>>(resultDto);
            Assert.Equal(expectedMovies, resultDto.Data.Count());
        }

        [Fact]
        public async Task GetMovieAsync_Returns_Expected_MovieDto()
        {
            //Arrange
            var movie = GenerateMovies(1)[0];
            _uowMock.Setup(x => x.Movies.GetMovieAsync(It.IsAny<int>(), true)).ReturnsAsync(movie);

            //Act
            var resultDto = await sut.GetMovieAsync(movie.Id, true);

            //Assert
            Assert.NotNull(resultDto);
            Assert.IsType<MovieDto>(resultDto);
            Assert.Equal(movie.Id, resultDto.Id);
        }

        [Fact]
        public async Task GetMovieDetailsAsync_Returns_Expected_MovieDetailDto()
        {
            //Arrange
            var movie = GenerateMovies(1)[0];
            _uowMock.Setup(x => x.Movies.GetMovieWithQueryOptionsAsync(1, It.IsAny<MovieQueryOptionsDto>())).ReturnsAsync(movie);

            //Act
            var resultDto = await sut.GetMovieDetailsAsync(movie.Id, new MovieQueryOptionsDto { IncludeDetails = true, IncludeGenre = true });

            //Assert
            Assert.NotNull(resultDto);
            Assert.IsType<MovieDetailDto>(resultDto);
            Assert.Equal(movie.Id, resultDto.Id);
            Assert.Equal(movie.MovieDetails.Synopsis, resultDto.MovieDetailsSynopsis);
        }






        
        public List<Movie> GenerateMovies(int numberOfMovies)
        {
            var movies = new List<Movie>();
            var faker = new Faker();
            var genreNames = new[]
{
                "Action",
                "Drama",
                "Comedy",
                "Horror",
                "Documentary",
                "Fantasy"
            };

            string[] languages = new[] { "English", "Swedish", "French", "German", "Japanese" };

            for (int i = 1; i <= numberOfMovies; i++)
            {
                var movie = new Movie
                {
                    Id = i,
                    Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Commerce.ProductName()),
                    Year = faker.Date.Past(20).Year,
                    Duration = faker.Random.Int(60, 180),
                    GenreId = faker.Random.Int(1, 10),
                    Genre = new Genre{ Name = faker.PickRandom(genreNames) },
                    MovieDetails = new MovieDetails
                    {
                        Synopsis = faker.Lorem.Sentence(20),
                        Language = faker.PickRandom(languages),
                        Budget = faker.Random.Int(20000, 1000000)
                    }
                };

                movies.Add(movie);
            }

            return movies;
        }

    }
}
