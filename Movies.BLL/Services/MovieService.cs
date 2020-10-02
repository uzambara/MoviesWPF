using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Abstractions.Models;
using  Movies.Abstractions.Services;
using Movies.DAL.Data.Domain;
using Movies.DAL.Data.Repositories;

namespace Movies.BLL.Services
{
    public class MovieService: IMovieService
    {
        private readonly BaseRepository<MovieEntity> _movieRepository;
        private readonly IMovieApiClient _movieApiClient;
        private readonly IMapper _mapper;
        public MovieService(BaseRepository<MovieEntity> movieRepository, IMovieApiClient movieApiClient, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _movieApiClient = movieApiClient;
            _mapper = mapper;
        }

        public async Task<List<Movie>> GetMoviesByNameAsync(string name)
        {
            var movies = await _movieApiClient.GetMoviesByNameAsync(name);
            var favoriteMovies = await GetFavoriteMoviesAsync();
            var query =
                from movie in movies
                join favoriteMovie in favoriteMovies on movie.ExternalId equals favoriteMovie.ExternalId
                select movie;
            query.ToList().ForEach(movie => movie.IsFavorite = true);

            return movies;
        }

        public async Task<List<Movie>> GetFavoriteMoviesAsync()
        {
            var movieEntities = await _movieRepository
                .GetAllWhere(movie => movie.IsFavorite)
                .ToListAsync();
            return _mapper.Map<List<Movie>>(movieEntities);
        }

        public async Task RemoveFromFavorite(int id)
        {
            var favoriteMovie = await _movieRepository.GetOneAsync(id);
            favoriteMovie.IsFavorite = false;
            await _movieRepository.SaveChangesAsync();
        }

        public async  Task ToggleFavorite(Movie movie)
        {
            var existsMovie = await _movieRepository.GetOneWhereAsync(m => m.ExternalId == movie.ExternalId);
            if (existsMovie != null)
            {
                existsMovie.IsFavorite = !existsMovie.IsFavorite;
                await _movieRepository.SaveChangesAsync();
                return;
            }

            var movieEntity = _mapper.Map<Movie, MovieEntity>(movie);
            movieEntity.IsFavorite = true;
            _movieRepository.Add(movieEntity);
            await _movieRepository.SaveChangesAsync();
        }
    }
}