using Movies.BLL.HttpClients.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.BLL.HttpClients.Contract
{
    public class GetMoviesResponse
    {
        public List<OmdbMovie> Search { get; set; }
        [JsonProperty("totalResults")]
        public string TotalResults { get; set; }
        public bool Response { get; set; }
        public string Error { get; set; }
    }
}
