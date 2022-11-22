using Apparatus.AOT.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace OneAPI.Models.Lib
{
    public class Request<T>
    {
        private int? limit = null;
        private int? page = null;
        private List<QueryParam<T>> queryParams;
        private SortDirection sortDirection;
        private string sortProperty;

        public Request()
        {
            queryParams = new List<QueryParam<T>>();
        }

        public Request<T> AddParam(KeyOf<T> paramName, string value, ParamType paramType = ParamType.EQUALS)
        {
            queryParams.Add(new QueryParam<T> { KeyOf = paramName, ParamType = paramType, Value = value });
            return this;
        }

        public Request<T> SetLimit(int limit)
        {
            this.limit = limit;
            return this;
        }

        public Request<T> SetPage(int page)
        {
            this.page = page;
            return this;
        }

        public Request<T> SetSort(KeyOf<T> sortProperty, SortDirection sortDir = SortDirection.ASCENDING)
        {
            this.sortProperty = sortProperty.Value;
            this.sortDirection = sortDir;
            return this;
        }

        public override string ToString()
        {
            var queryParams = new List<string>();

            if (this.sortProperty != null)
            {
                string sortDirection = this.sortDirection == SortDirection.ASCENDING ? "asc" : "desc";
                queryParams.Add($"sort={this.sortProperty}:{sortDirection}");
            }

            if (this.limit != null)
            {
                queryParams.Add($"limit={this.limit}");
            }

            if (this.page != null)
            {
                queryParams.Add($"page={this.page}");
            }

            if (this.queryParams.Any())
            {
                queryParams.AddRange(this.queryParams.Select(qp =>
                {
                    var property = qp.KeyOf.Value;
                    var value = "";

                    switch (qp.ParamType)
                    {
                        case ParamType.EQUALS:
                        case ParamType.INCLUDE:
                            value = $"={qp.Value}";
                            break;
                        case ParamType.EXCLUDE:
                        case ParamType.NEQUALS:
                            value += $"!={qp.Value}";
                            break;
                        case ParamType.NEXISTS:
                            property = $"!{property}";
                            break;
                        case ParamType.REGEX:
                            value = $"=/{qp.Value}/i";
                            break;
                        case ParamType.NREGEX:
                            value = $"!=/{qp.Value}/i";
                            break;
                        case ParamType.LESS:
                            value = $"<{qp.Value}";
                            break;
                        case ParamType.LTE:
                            value = $"<={qp.Value}";
                            break;
                        case ParamType.GREATER:
                            value = $">{qp.Value}";
                            break;
                        case ParamType.GTE:
                            value = $">={qp.Value}";
                            break;
                    }

                    return $"{property}{value}";
                }));
            }

            var uri = $"{typeof(T).Name}";

            if (queryParams.Any())
            {
                uri += $"?{string.Join("&", queryParams)}";
            }

            return uri;
        }
    }
}
