using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Abstractions.Models;

namespace Movies.Abstractions.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMoviesByNameAsync(string name);
        Task<List<Movie>> GetFavoriteMoviesAsync();
        Task RemoveFromFavorite(int id);
        Task ToggleFavorite(Movie movie);
    }
}