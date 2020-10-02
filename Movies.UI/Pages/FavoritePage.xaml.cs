using AutoMapper;
using Movies.UI.Components;
using Movies.UI.Navigation;
using Movies.UI.VewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;

namespace Movies.UI.Pages
{
    /// <summary>
    /// Interaction logic for FavoritePage.xaml
    /// </summary>
    public partial class FavoritePage : Page
    {
        private readonly PageNavigator _pageNavigator;
        private readonly FavoritePageViewModel _favoritePageViewModel;

        public FavoritePage(PageNavigator pageNavigator, FavoritePageViewModel favoritePageViewModel)
        {
            InitializeComponent();

            _pageNavigator = pageNavigator;
            _favoritePageViewModel = favoritePageViewModel;

            DataContext = favoritePageViewModel;

            GoToSearchButton.Click += GoToSearchButton_Click;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, EventArgs args)
        {
            await _favoritePageViewModel.LoadFavoriteAsync();
        }

        private void GoToSearchButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigator.GoTo(PageType.Search);
        }
    }
}
