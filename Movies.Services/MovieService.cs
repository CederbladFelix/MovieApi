﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Exceptions;
using Movies.Core.Models.DTOs;
using Movies.Core.Models.DTOs.Validated;
using Movies.Core.Models.Entities;
using Movies.Services.Contracts;

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

        public async Task<PagedResultDto<MovieDto>> GetMoviesAsync(PaginationOptionsDto paginationOptions, bool includeGenre = false)
        {
            var totalItems = await _unitOfWork.Movies.MovieCountAsync(); 

            var movies = await _unitOfWork.Movies.GetMoviesAsync(paginationOptions, includeGenre);

            var moviesDto = _mapper.Map<IEnumerable<MovieDto>>(movies);

            return new PagedResultDto<MovieDto>
                (
                    data: moviesDto,
                    totalItems: totalItems,
                    currentPage: paginationOptions.CurrentPage,
                    pageSize: paginationOptions.PageSize
                );
        }

        public async Task<MovieDto> GetMovieAsync(int id, bool includeGenre = false)
        {
            var movie = await _unitOfWork.Movies.GetMovieAsync(id, includeGenre: true);
            if (movie == null)
                throw new MovieNotFoundException(id);

            var movieDto = _mapper.Map<MovieDto>(movie);

            return movieDto;
        }

        public async Task<MovieDetailDto> GetMovieDetailsAsync(int id, MovieQueryOptionsDto options)
        {
            var movie = await _unitOfWork.Movies.GetMovieWithQueryOptionsAsync(id, options);

            if (movie == null)
                throw new MovieNotFoundException(id);

            var movieDto = _mapper.Map<MovieDetailDto>(movie);

            return movieDto;
        }

        public async Task PutMovieAsync(int id, MovieUpdateDto dto)
        {
            var movie = await _unitOfWork.Movies.GetMovieWithQueryOptionsAsync(id, new MovieQueryOptionsDto { IncludeDetails = true, IncludeGenre = true });

            if (movie == null)
                throw new MovieNotFoundException(id);

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
                    throw new MovieNotFoundException(id);
                else
                    throw;
            }
        }

        public async Task<MoviePatchDto> GetMoviePatchDtoAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetMovieWithQueryOptionsAsync(id, new MovieQueryOptionsDto { IncludeDetails = true, IncludeGenre = true });

            if (movie == null)
                throw new MovieNotFoundException(id);

            var movieToPatchDto = _mapper.Map<MoviePatchDto>(movie);

            return movieToPatchDto;

        }

        public async Task SavePatchedMovie(int id, MoviePatchDto movieToPatchDto)
        {
            var movie = await _unitOfWork.Movies.GetMovieWithQueryOptionsAsync(id, new MovieQueryOptionsDto { IncludeDetails = true, IncludeGenre = true });

            if (movie == null)
                throw new MovieNotFoundException(id);

            _mapper.Map(movieToPatchDto, movie);
            if (!string.IsNullOrWhiteSpace(movieToPatchDto.Genre))
            {
                var genre = await _unitOfWork.Movies.GetGenreByNameAsync(movieToPatchDto.Genre);
                movie.Genre = genre;
            }

            await _unitOfWork.CompleteAsync();
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

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetMovieAsync(id, includeGenre: true);

            if (movie == null)
                throw new MovieNotFoundException(id);

            _unitOfWork.Movies.Delete(movie);
            await _unitOfWork.CompleteAsync();
        }

    }
}
