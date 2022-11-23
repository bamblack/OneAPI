// See https://aka.ms/new-console-template for more information
using Apparatus.AOT.Reflection;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OneAPI.CLI.Options;
using OneAPI.SDK.Contracts;
using OneAPI.SDK.Models.API;
using OneAPI.SDK.Models.Lib;
using OneAPI.SDK.Services;

internal class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHttpClient();
                services.AddTransient<ITheOneService, TheOneService>();
            })
            .Build();

        var scope = host.Services.CreateScope();
        var provider = scope.ServiceProvider;
        var oneService = provider.GetService<ITheOneService>();
        oneService.Configure("Pp-ONiK0UvjaX5OyYZcv");

        await Parser.Default
            .ParseArguments<QueryOptions>(args)
            .WithParsedAsync<QueryOptions>(async (opts) => {
                string output = string.Empty;
                switch (opts.Type.ToLower())
                {
                    case "book":
                        output = await RunRequest<Book>(opts, oneService);
                        break;
                    case "chapter":
                        output = await RunRequest<Chapter>(opts, oneService);
                        break;
                    case "character":
                        output = await RunRequest<Character>(opts, oneService);
                        break;
                    case "movie":
                        output = await RunRequest<Movie>(opts, oneService);
                        break;
                    case "quote":
                        output = await RunRequest<Quote>(opts, oneService);
                        break;
                }

                if (output != string.Empty)
                {
                    Console.Write("Results: ");
                    Console.WriteLine(output);
                    Console.WriteLine("");
                }
            });
    }

    static Request<T> BuildRequest<T>(QueryOptions options)
    {
        var request = new Request<T>();

        if (!string.IsNullOrWhiteSpace(options.SortProp))
        {
            var validSortProp = KeyOf<T>.TryParse(options.SortProp, out var sortProp);
            if (validSortProp)
            {
                var sortDir = options.SortDir?.ToLower() == "desc" ?
                    SortDirection.DESCENDING :
                    SortDirection.ASCENDING;

                request.SetSort(sortProp, sortDir);
            }
            else
            {
                Console.WriteLine($"Invalid property {options.SortProp} specified for sort. Ignoring this flag.");
            }
        }

        if (options.Limit != null) 
        {
            request.SetLimit(options.Limit.Value);
        }

        // TODO: filter stuff

        return request;
    }

    static async Task<string> RunRequest<T>(QueryOptions options, ITheOneService oneService)
    {
        var request = BuildRequest<T>(options);
        var results = Enumerable.Empty<T>();

        if (options.AllRecords)
        {
            results = await oneService.GetAll(request);
        }
        else
        {
            results = await oneService.Get(request);
        }

        var stringResult = JsonConvert.SerializeObject(results, options.Pretty ? Formatting.Indented : Formatting.None);
        return stringResult;
    }



    /// <summary>
    /// This method is never called. It does some black magic to help KeyOf generate its source dictionaries for doing its stuff
    /// because we only use it in generics, so it doesn't do it without this
    /// :shrug:
    /// https://github.com/byme8/Apparatus.AOT.Reflection#limitations
    /// </summary>
    private void DontCallMe()
    {
        var book = new Book();
        book.GetProperties();

        var chapter = new Chapter();
        chapter.GetProperties();

        var character = new Character();
        character.GetProperties();

        var movie = new Movie();
        movie.GetProperties();

        var quote = new Quote();
        quote.GetProperties();
    }
}