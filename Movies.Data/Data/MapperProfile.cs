using AutoMapper;
using Movies.Core.Models.DTOs;
using Movies.Core.Models.Entities;

namespace Movies.Data.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Movie, MovieDto>();

            CreateMap<Movie, MovieDetailDto>()
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(ma => ma.Actor)));

            CreateMap<Movie, MovieCreateDto>();
            CreateMap<MovieCreateDto, Movie>()
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.MovieDetails, opt => opt.MapFrom(src => new MovieDetails
                {
                    Synopsis = src.MovieDetailsSynopsis,
                    Language = src.MovieDetailsLanguage,
                    Budget = src.MovieDetailsBudget
                }));

            CreateMap<Movie, MovieUpdateDto>();
            CreateMap<MovieUpdateDto, Movie>()
                 .ForMember(dest => dest.Genre, opt => opt.Ignore());

            CreateMap<Review, ReviewDto>();
            CreateMap<Actor, ActorDto>();

            CreateMap<MovieActor, MovieActorDto>();


            CreateMap<MovieActor, MovieActorCreateDto>().ReverseMap();
            CreateMap<MovieActor, MovieActorCreateWithActorIdDto>().ReverseMap();

        }
    }


}
