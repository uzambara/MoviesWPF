using AutoMapper;
using Movies.Abstractions.Models;
using Movies.BLL.HttpClients.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Movies.Abstractions.Services;
using Movies.UI.Events;

namespace Movies.UI.VewModels
{
    public class FavoriteCardViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly IEventManager _eventManager;
        private Movie _movie;

        public FavoriteCardViewModel(IMovieService movieService, IEventManager eventManager)
        {
            _movieService = movieService;
            _eventManager = eventManager;
        }

        public Movie Movie 
        { 
            get => _movie;
            set
            {
                _movie = value;
                OnPropertyChanged("Movie");
            }
        }

        public async Task RemoveFromFavorite()
        {
            await _movieService.RemoveFromFavorite(Movie.Id);
            _eventManager.NotifyRemoveFromFavorite(Movie.Id);
        }
    }
}
