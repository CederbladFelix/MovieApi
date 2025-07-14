using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.DTOs;
using Movies.Core.Models.Entities;
using Movies.Services.Contracts.Contracts;

namespace Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesAsync(bool includeGenre = false)
        {
            var movies = await _unitOfWork.Movies.GetMoviesAsync(includeGenre);
            var moviesDto = _mapper.Map<IEnumerable<MovieDto>>(movies);

            return moviesDto;
        }

        public async Task<MovieDto> GetMovieAsync(int id, bool includeGenre = false)
        {
            var movie = await _unitOfWork.Movies.GetMovieAsync(id, includeGenre: true);
            if (movie == null)
                return null!;

            var movieDto = _mapper.Map<MovieDto>(movie);

            return movieDto;
        }

        public async Task<MovieDetailDto> GetMovieDetailsAsync(int id, MovieQueryOptions options)
        {
            var movie = await _unitOfWork.Movies.GetMovieWithQueryOptionsAsync(id, options);

            if (movie == null)
                return null!;

            var movieDto = _mapper.Map<MovieDetailDto>(movie);

            return movieDto;
        }

        public async Task<bool> PutMovieAsync(int id, MovieUpdateDto dto)
        {
            var movie = await _unitOfWork.Movies.GetMovieAsync(id, includeGenre: true);

            if (movie == null)
                return false;

            var genre = await _unitOfWork.Movies.GetGenreByNameAsync(dto.Genre);

            _mapper.Map(dto, movie);
            movie.Genre = genre;

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.Movies.AnyMovieAsync(id))
                    return false;
                else
                    throw;
            }

            return true;
        }

        public async Task<MovieDto> PostMovieAsync(MovieCreateDto dto)
        {
            var genre = await _unitOfWork.Movies.GetGenreByNameAsync(dto.Genre);

            var movie = _mapper.Map<Movie>(dto);
            movie.Genre = genre;

            _unitOfWork.Movies.Add(movie);
            await _unitOfWork.CompleteAsync();

            var movieDto = _mapper.Map<MovieDto>(movie);

            return movieDto;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetMovieAsync(id, includeGenre: true);

            if (movie == null)
                return false;

            _unitOfWork.Movies.Delete(movie);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
