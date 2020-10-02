using AutoMapper;
using Movies.Abstractions.Models;
using Movies.Abstractions.Services;
using Movies.BLL.HttpClients.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Movies.UI.Events;

namespace Movies.UI.VewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        private string _searchInput;
        private List<Movie> _movies = new List<Movie>();
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;

        public SearchPageViewModel(IMovieService movieService, IMapper mapper, IEventManager eventManager)
        {
            _mapper = mapper;
            _movieService = movieService;
            Movies = GetMoviesData().Result;

            eventManager.OnRemoveFromFavorite += id =>
            {
                Movies = Movies.Select(movie =>
                {
                    movie.IsFavorite = movie.Id != id && movie.IsFavorite;
                    return movie;
                }).ToList();
            };
        }

        public string SearchInput 
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
            }
        }

        public List<Movie> Movies
        {
            get => _movies;
            set
            {
                _movies = value;
                OnPropertyChanged("Movies");
            }
        }

        public async Task SearchMovies()
        {
            Movies = await _movieService.GetMoviesByNameAsync(_searchInput);
        }

        public async void ToggleMovieFavorite(string externalId)
        {
            var favoriteMovie = Movies.FirstOrDefault(movie => movie.ExternalId == externalId);
            if (favoriteMovie == null) return;

            await _movieService.ToggleFavorite(favoriteMovie);
            favoriteMovie.IsFavorite = !favoriteMovie.IsFavorite;
            Movies = Movies.ToList();
        }

        private async Task<List<Movie>> GetMoviesData()
        {
            var json = File.ReadAllText(@"G:\Projects\Movies\Movies.UI\moviesData.json");

            var movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            var favoriteMovies = await _movieService.GetFavoriteMoviesAsync();
            var query =
                from movie in movies
                join favoriteMovie in favoriteMovies on movie.ExternalId equals favoriteMovie.ExternalId
                select movie;
            query.ToList().ForEach(movie => movie.IsFavorite = true);

            return movies;

        }
    }
}
