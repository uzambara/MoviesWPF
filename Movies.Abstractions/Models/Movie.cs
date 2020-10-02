using Newtonsoft.Json;

namespace Movies.Abstractions.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public bool IsFavorite { get; set; }

        [JsonIgnore]
        public string ViewTitle { get => $"{Title} ({Year})\n\nActors: {Actors}\n\nWriter: {Writer}"; }
    }
}