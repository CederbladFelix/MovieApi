using Movies.Core.DomainContracts;
using Movies.Data.Data;

namespace Movies.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieApiContext _movieApiContext;
        private readonly Lazy<IMovieRepository> _movieRepository;
        private readonly Lazy<IActorRepository> _actorRepository;
        private readonly Lazy<IReviewRepository> _reviewRepository;
        public IMovieRepository Movies => _movieRepository.Value;
        public IActorRepository Actors => _actorRepository.Value;
        public IReviewRepository Reviews => _reviewRepository.Value;

        public UnitOfWork(MovieApiContext movieApiContext)
        {
            _movieRepository = new Lazy<IMovieRepository>(() => new MovieRepository(movieApiContext));
            _actorRepository = new Lazy<IActorRepository>(() => new ActorRepository(movieApiContext));
            _reviewRepository = new Lazy<IReviewRepository>(() => new ReviewRepository(movieApiContext));
            _movieApiContext = movieApiContext;
        }

        public async Task CompleteAsync() => await _movieApiContext.SaveChangesAsync();
        
    }
}
