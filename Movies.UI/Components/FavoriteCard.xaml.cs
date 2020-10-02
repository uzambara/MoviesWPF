using Movies.UI.VewModels;
using System.Windows.Controls;
using Movies.UI.Events;

namespace Movies.UI.Components
{
    /// <summary>
    /// Interaction logic for FavoriteCard.xaml
    /// </summary>
    public partial class FavoriteCard : UserControl
    {
        private FavoriteCardViewModel _favoriteCardViewModel;

        public FavoriteCard()
        {
            InitializeComponent();

            RemoveFromFavoriteButton.Click += async (sender, args) =>
            {
                if(_favoriteCardViewModel != null)
                    await _favoriteCardViewModel.RemoveFromFavorite();
            };
        }
        public int MovieId => _favoriteCardViewModel.Movie.Id;

        public FavoriteCardViewModel FavoriteCardViewModel
        {
            set
            {
                DataContext = value;
                _favoriteCardViewModel = value;
            }
        }
    }
}
