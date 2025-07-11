namespace Movies.Services.Contracts
{
    public interface IServiceManager
    {
        IMovieService MovieService { get; }
        IActorService ActorService { get; }
        IReviewService ReviewService { get; }
    }
}
