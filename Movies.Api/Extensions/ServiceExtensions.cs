using Movies.Core.DomainContracts;
using Movies.Data.Repositories;
using Movies.Services;
using Movies.Services.Contracts;

namespace Movies.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped(provider => new Lazy<IMovieRepository>(() => provider.GetRequiredService<IMovieRepository>()));
            services.AddScoped(provider => new Lazy<IActorRepository>(() => provider.GetRequiredService<IActorRepository>()));
            services.AddScoped(provider => new Lazy<IReviewRepository>(() => provider.GetRequiredService<IReviewRepository>()));
        }
        public static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped(_ => new Lazy<IMovieService>(() => _.GetRequiredService<IMovieService>()));
            services.AddScoped(provider => new Lazy<IActorService>(() => provider.GetRequiredService<IActorService>()));
            services.AddScoped(provider => new Lazy<IReviewService>(() => provider.GetRequiredService<IReviewService>()));
        }
    }
}
