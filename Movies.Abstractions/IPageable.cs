using System.Collections.Generic;

namespace Movies.Abstractions
{
    public class Pageable<T>
    {
        public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasMore { get; set; }
    }
}