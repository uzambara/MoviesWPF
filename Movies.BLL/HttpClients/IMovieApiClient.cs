using System.Threading.Tasks;
using Movies.Abstractions;
using Movies.Abstractions.Models;

namespace Movies.BLL.Services
{
    public interface IMovieApiClient
    {
        Task<Pageable<Movie>> GetPageableMoviesByNameAsync(string name, int pageNumber = 1);
    }
}
