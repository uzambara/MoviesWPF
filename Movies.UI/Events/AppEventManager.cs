namespace Movies.UI.Events
{
    public class AppEventManager : IEventManager
    {
        public event RemoveFromFavoriteHandler OnRemoveFromFavorite;
        public void NotifyRemoveFromFavorite(int movieId)
        {
            OnRemoveFromFavorite?.Invoke(movieId);
        }
    }
}