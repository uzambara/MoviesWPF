using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Movies.Abstractions.Errors;
using Movies.Abstractions.Models;
using Movies.BLL.HttpClients.Contract;
using Movies.BLL.HttpClients.Data;
using Newtonsoft.Json;

namespace Movies.BLL.Services
{
    public class OmdbApiClientService: IMovieApiClient
    {
        private readonly IMapper _mapper;
        private HttpClient _httpClient;
        private string _apiBaseAddress = "http://www.omdbapi.com/";
        private string _apiKey = "&apikey=2ede91da";

        public OmdbApiClientService(IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_apiBaseAddress)
            };
        }
        public async Task<List<Movie>> GetMoviesByNameAsync(string name)
        {
            string responseContent;
            try
            {
                using (var response = await _httpClient.GetAsync($"?s={name}{_apiKey}"))
                {
                    response.EnsureSuccessStatusCode();
                    responseContent = await response.Content.ReadAsStringAsync();
                }

                var searchResponse = JsonConvert.DeserializeObject<GetMoviesResponse>(responseContent);
                if (!searchResponse.Response)
                    throw new CommonException(ErrorCode.RemoteApiNotFoundMovies, searchResponse.Error);

                var extendedMovies = new List<OmdbMovieExtended>();
                foreach (var movie in searchResponse.Search)
                {
                    using (var response = await _httpClient.GetAsync($"?i={movie.imdbID}{_apiKey}"))
                    {
                        response.EnsureSuccessStatusCode();
                        responseContent = await response.Content.ReadAsStringAsync();
                    }

                    var extendedMovieInfo = JsonConvert.DeserializeObject<OmdbMovieExtended>(responseContent);
                    extendedMovies.Add(extendedMovieInfo);
                }

                var result = _mapper.Map<List<Movie>>(extendedMovies);
                //var serializedData = JsonConvert.SerializeObject(result);
                //File.WriteAllText(@"G:\Projects\Movies\Movies.UI\moviesData.json", serializedData);
                return result;
            }
            catch (CommonException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new CommonException(ErrorCode.RemoteApiRequestError, ex.Message);
            }
        }
    }
}