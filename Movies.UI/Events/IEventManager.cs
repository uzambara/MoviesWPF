namespace Movies.UI.Events
{
    public delegate void RemoveFromFavoriteHandler(int movieId);


    public interface IEventManager
    {
        event RemoveFromFavoriteHandler OnRemoveFromFavorite;
        void NotifyRemoveFromFavorite(int movieId);
    }
}