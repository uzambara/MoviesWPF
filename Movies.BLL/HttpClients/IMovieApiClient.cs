using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Abstractions;
using Movies.Abstractions.Models;

namespace Movies.BLL.Services
{
    public interface IMovieApiClient
    {
        Task<List<Movie>> GetMoviesByNameAsync(string name);
        Task<Pageable<Movie>> GetPageableMoviesByNameAsync(string name, int pageNumber);
    }
}