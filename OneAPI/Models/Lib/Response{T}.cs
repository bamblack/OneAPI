using System.Collections.Generic;

namespace OneAPI.SDK.Models.Lib
{
    internal class Response<T>
    {
        public IEnumerable<T> docs { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int page { get; set; }
        public int pages { get; set; }
        public int total { get; set; }
    }
}
