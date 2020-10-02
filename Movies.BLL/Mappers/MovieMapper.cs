using AutoMapper;
using Movies.Abstractions.Models;
using Movies.BLL.HttpClients.Data;
using Movies.DAL.Data.Domain;

namespace Movies.BLL.Mappers
{
    public class MovieMapper: Profile
    {
        public MovieMapper()
        {
            CreateMap<MovieEntity, Movie>();
            CreateMap<OmdbMovie, Movie>()
                .ForMember(
                    dest => dest.ExternalId,
                    opt => opt.MapFrom(src => src.imdbID));
            CreateMap<OmdbMovieExtended, Movie>()
                .ForMember(
                    dest => dest.ExternalId,
                    opt => opt.MapFrom(src => src.imdbID));


            CreateMap<Movie, MovieEntity>();
        }
    }
}