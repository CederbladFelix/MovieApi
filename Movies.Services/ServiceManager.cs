using Movies.Services.Contracts;

namespace Movies.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMovieService> _movieService;
        private readonly Lazy<IActorService> _actorService;
        private readonly Lazy<IReviewService> _reviewService;

        public IMovieService MovieService => _movieService.Value;
        public IActorService ActorService => _actorService.Value;
        public IReviewService ReviewService => _reviewService.Value;

        public ServiceManager(Lazy<IMovieService> movieService, Lazy<IActorService> actorService, Lazy<IReviewService> reviewService)
        {
            _movieService = movieService;
            _actorService = actorService;
            _reviewService = reviewService;
        }
    }
}
