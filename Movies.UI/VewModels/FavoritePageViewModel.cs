
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Movies.Abstractions.Models;
using Movies.Abstractions.Services;
using Movies.UI.Components;
using Movies.UI.Events;
using Movies.UI.Factories;

namespace Movies.UI.VewModels
{
    public class FavoritePageViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly IEventManager _eventManager;
        private readonly FavoriteCardFactory _favoriteCardFactory;
        private List<FavoriteCard> _favoriteCards;

        public FavoritePageViewModel(
            IMovieService movieService,
            IEventManager eventManager,
            FavoriteCardFactory favoriteCardFactory)
        {
            _movieService = movieService;
            _eventManager = eventManager;
            _favoriteCardFactory = favoriteCardFactory;

            _eventManager.OnRemoveFromFavorite += RemoveFavoriteMovieHandler;
        }

        public List<FavoriteCard> FavoriteCards
        {
            get => _favoriteCards;
            set
            {
                _favoriteCards = value;
                OnPropertyChanged("FavoriteCards");
            }
        }

        public async Task LoadFavoriteAsync()
        {
            var favoriteMovies = await _movieService.GetFavoriteMoviesAsync();
            FavoriteCards = favoriteMovies.Select(_favoriteCardFactory.Create).ToList();
        }

        private async void RemoveFavoriteMovieHandler(int id)
        {
            await _movieService.RemoveFromFavorite(id);
            FavoriteCards = FavoriteCards.Where(card => card.MovieId != id).ToList();
        }
    }
}
