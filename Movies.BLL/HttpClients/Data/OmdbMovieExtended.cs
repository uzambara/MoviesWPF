using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.BLL.HttpClients.Data
{
    public class OmdbMovieExtended : OmdbMovie
    {
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
    }
}
