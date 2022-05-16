using System.Collections.Generic;

namespace IoLocate.Api.Client.ConsoleApp.Models
{
    public class PagedList<T> where T : class
    {
        public IList<T> Source { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
    }
}
