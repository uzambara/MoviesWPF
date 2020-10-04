using Movies.UI.Navigation;
using Movies.UI.VewModels;
using System.Windows;
using System.Windows.Controls;

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

        private async void MoviesDataGrid_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var isScrolledToBottom = e.VerticalOffset + e.ViewportHeight >= e.ExtentHeight;
            var isScrolled = e.VerticalChange > 0;
            if (isScrolled && isScrolledToBottom)
            {
                try
                {
                    Loader.StartLoading();
                    SearchButton.IsEnabled = false;
                    await _searchPageViewModel.LoadMore();
                }
                finally
                {
                    Loader.FinishLoading();
                    SearchButton.IsEnabled = true;
                }
            }
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
