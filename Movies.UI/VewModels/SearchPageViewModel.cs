using AutoMapper;
using Movies.Abstractions.Models;
using Movies.Abstractions.Services;
using Movies.BLL.HttpClients.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Movies.Abstractions;
using Movies.UI.Events;

namespace Movies.UI.VewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        private static object _syncObject = "";
        private string _searchInput;
        private string _foundedMoviesCountText;
        private List<Movie> _movies = new List<Movie>();
        private Pageable<Movie> _pageableMovies = new Pageable<Movie>();
        private readonly IMovieService _movieService;

        public SearchPageViewModel(IMovieService movieService, IEventManager eventManager)
        {
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
        public string FoundedMoviesCountText
        {
            get => _foundedMoviesCountText;
            set
            {
                _foundedMoviesCountText = value;
                OnPropertyChanged("FoundedMoviesCountText");
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
            if (Monitor.TryEnter(_syncObject))
            {
                _pageableMovies = await _movieService.GetPageableMoviesByNameAsync(_searchInput, 1);
                FoundedMoviesCountText = $"Фильмов {_pageableMovies.Data.Count} из {_pageableMovies.TotalCount}.";
                Movies = _pageableMovies.Data;
            }
        }

        public async Task LoadMore()
        {
            if (Monitor.TryEnter(_syncObject))
            {
                if (_pageableMovies.HasMore)
                {
                    _pageableMovies = await _movieService.GetPageableMoviesByNameAsync(_searchInput, _pageableMovies.PageNumber + 1);
                    if (_pageableMovies.Data.Count > 0)
                    {
                        Movies.AddRange(_pageableMovies.Data);
                        Movies = Movies.ToList();
                        FoundedMoviesCountText = $"Фильмов {Movies.Count} из {_pageableMovies.TotalCount}.";
                    }
                }
            }
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