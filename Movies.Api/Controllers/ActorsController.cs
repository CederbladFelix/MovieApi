using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.DomainContracts;
using Movies.Core.Models.DTOs;
using Movies.Core.Models.Entities;
using Swashbuckle.AspNetCore.Annotations;


namespace Movies.Api.Controllers
{
    [Route("api/movies/{movieId}/actors")]
    [ApiController]
    [Produces("application/json")]
    public class ActorsController(IUnitOfWork unitOfWork, /*MovieApiContext context,*/ IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        //private readonly MovieApiContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [SwaggerOperation(Summary = "Connect actor to movie", Description = "Connect actor to a movie.", Tags = ["Movie", "Actor"])]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieActorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieActorDto>> AddActorToMovie(int movieId, [FromBody] MovieActorCreateWithActorIdDto dto)
        {
            var actorExists = await _unitOfWork.Actors.AnyAsync(dto.ActorId);
            var movieExists = await _unitOfWork.Movies.AnyAsync(movieId);

            if (!actorExists || !movieExists)
                return NotFound();

            var actorAlreadyInMovie = await _unitOfWork.Actors.InMovieAsync(dto.ActorId, movieId);
                
                /*await _context.MovieActors
                .AnyAsync(ma => ma.ActorId == dto.ActorId && ma.MovieId == movieId);*/

            if (actorAlreadyInMovie)
                return Conflict();

            var movieActor = _mapper.Map<MovieActor>(dto);
            movieActor.MovieId = movieId;

            //_context.MovieActors.Add(movieActor);
            _unitOfWork.Actors.AddMovieActor(movieActor);
            await _unitOfWork.CompleteAsync();

            return Created(
                $"/api/movies/{movieId}/actors/{dto.ActorId}",
                _mapper.Map<MovieActorDto>(movieActor)
            );
        }

        [HttpPost("with-actor/{actorId}")]
        [SwaggerOperation(Summary = "Connect actor to movie", Description = "Connect actor to a movie.", Tags = ["Movie", "Actor"])]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieActorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieActorDto>> AddActorToMovie(int movieId, int actorId, [FromBody] MovieActorCreateDto dto)
        {
            var actorExists = await _unitOfWork.Actors.AnyAsync(actorId);
            var movieExists = await _unitOfWork.Movies.AnyAsync(movieId);

            if (!actorExists || !movieExists)
                return NotFound();

            var actorAlreadyInMovie = await _unitOfWork.Actors.InMovieAsync(actorId, movieId);

            if (actorAlreadyInMovie)
                return Conflict();

            var movieActor = _mapper.Map<MovieActor>(dto);
            movieActor.MovieId = movieId;
            movieActor.ActorId = actorId;

            _unitOfWork.Actors.AddMovieActor(movieActor);
            await _unitOfWork.CompleteAsync();

            return Created(
                $"/api/movies/{movieId}/actors/{actorId}",
                _mapper.Map<MovieActorDto>(movieActor)
            );
        }
    }
}
