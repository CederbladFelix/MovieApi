using AutoMapper;
using Movies.Core.DomainContracts;
using Movies.Core.Exceptions;
using Movies.Core.Models.DTOs;
using Movies.Core.Models.DTOs.Validated;
using Movies.Core.Models.Entities;
using Movies.Services.Contracts;

namespace Movies.Services
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MovieActorDto> AddActorToMovieAsync(int movieId, MovieActorCreateWithActorIdDto dto)
        {
            var actorExists = await _unitOfWork.Actors.AnyActorAsync(dto.ActorId);
            var movieExists = await _unitOfWork.Movies.AnyMovieAsync(movieId);

            if (!actorExists || !movieExists)
                throw new NotFoundException("Actor or Movie does not exist");

            var actorAlreadyInMovie = await _unitOfWork.Actors.ActorInMovieAsync(dto.ActorId, movieId);

            if (actorAlreadyInMovie)
                throw new ActorAlreadyInMovieBadRequestException(dto.ActorId, movieId);

            var movieActor = _mapper.Map<MovieActor>(dto);
            movieActor.MovieId = movieId;

            _unitOfWork.Actors.AddMovieActor(movieActor);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<MovieActorDto>(movieActor);

        }

        public async Task<MovieActorDto> AddActorToMovieAsync(int movieId, int actorId, MovieActorCreateDto dto)
        {
            var actorExists = await _unitOfWork.Actors.AnyActorAsync(actorId);
            var movieExists = await _unitOfWork.Movies.AnyMovieAsync(movieId);

            if (!actorExists || !movieExists)
                throw new NotFoundException("Actor or Movie does not exist");

            var actorAlreadyInMovie = await _unitOfWork.Actors.ActorInMovieAsync(actorId, movieId);

            if (actorAlreadyInMovie)
                throw new ActorAlreadyInMovieBadRequestException(actorId, movieId);

            var movieActor = _mapper.Map<MovieActor>(dto);
            movieActor.MovieId = movieId;
            movieActor.ActorId = actorId;

            _unitOfWork.Actors.AddMovieActor(movieActor);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<MovieActorDto>(movieActor);
        }

    }
}
