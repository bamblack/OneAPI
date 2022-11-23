using CommandLine;

namespace OneAPI.CLI.Options
{
    [Verb("query", HelpText = "Query the One API")]
    internal class QueryOptions
    {
        [Option('a', "all", Required = false, HelpText = "Set query to return all records.")]
        public bool AllRecords { get; set; }

        [Option('f', "filter", Required = false, HelpText = "Defin the filters for your query", Separator = ';')]
        public IEnumerable<string> Filter { get; set; }

        [Option('l', "limit", Required = false, HelpText = "Define the number of records you want to pull back")]
        public int? Limit { get; set; }

        [Option('p', "pretty", Required = false, HelpText = "Set output to be prettified.")]
        public bool Pretty { get; set; }

        [Option('d', "sortDir", Required = false, HelpText = "Define the sort direction for your query. Valid values are 'Asc' or 'Desc'")]
        public string SortDir { get; set; }

        [Option('s', "sortProp", Required = false, HelpText = "Define the sort property for your query")]
        public string SortProp { get; set; }

        [Option('t', "type", Required = true, HelpText = "Set the type you want to query by. Valid options are 'Book', 'Chapter', 'Character', 'Movie', and 'Quote'")]
        public string Type { get; set; }
    }
}
