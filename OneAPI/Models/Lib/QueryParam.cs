using Apparatus.AOT.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAPI.Models.Lib
{
    public class QueryParam<T>
    {
        public KeyOf<T> KeyOf { get; set; }
        public ParamType ParamType { get; set; }
        public string Value { get; set; }
    }
}
