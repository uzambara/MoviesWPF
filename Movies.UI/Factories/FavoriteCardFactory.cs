using System;
using Microsoft.Extensions.DependencyInjection;
using Movies.Abstractions.Models;
using Movies.UI.Components;
using Movies.UI.VewModels;

namespace Movies.UI.Factories
{
    public class FavoriteCardFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FavoriteCardFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public FavoriteCard Create(Movie movie)
        {
            var cardViewModel = _serviceProvider.GetRequiredService<FavoriteCardViewModel>();
            cardViewModel.Movie = movie;
            var result = _serviceProvider.GetRequiredService<FavoriteCard>();
            result.FavoriteCardViewModel = cardViewModel;
            return result;
        }
    }
}