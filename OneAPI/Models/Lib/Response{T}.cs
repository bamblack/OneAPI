using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAPI.Models.Lib
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
