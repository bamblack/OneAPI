using Apparatus.AOT.Reflection;

namespace OneAPI.SDK.Models.Lib
{
    public class QueryParam<T>
    {
        public KeyOf<T> KeyOf { get; set; }
        public ParamType ParamType { get; set; }
        public string Value { get; set; }
    }
}
