using Movies.Abstractions.Services;
using Movies.UI.Navigation;
using Movies.UI.VewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Movies.UI.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        private readonly PageNavigator _pageNavigator;
        private readonly SearchPageViewModel _searchPageViewModel;

        public SearchPage(PageNavigator pageNavigator, SearchPageViewModel searchPageViewModel)
        {
            _pageNavigator = pageNavigator;
            _searchPageViewModel = searchPageViewModel;

            InitializeComponent();

            DataContext = searchPageViewModel;

            GoToFavoriteButton.Click += GoToFavoriteButton_Click;
            SearchButton.Click += SearchButton_Click;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Loader.StartLoading();
                SearchButton.IsEnabled = false;
                await _searchPageViewModel.SearchMovies();
            }
            finally
            {
                Loader.FinishLoading();
                SearchButton.IsEnabled = true;
            }
        }

        private void GoToFavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigator.GoTo(PageType.Favorite);
        }

        private void AddToFavorite(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                var externalId = button.Uid;
                _searchPageViewModel.ToggleMovieFavorite(externalId);
            }
        }
    }
}
