// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OneAPI.Contracts;
using OneAPI.Models.API;
using OneAPI.Models.Lib;
using OneAPI.Services;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => {
        services.AddHttpClient();
        services.AddTransient<ISDK, SDK>();
    })
    .Build();

var scope = host.Services.CreateScope();
var provider = scope.ServiceProvider;
var sdk = provider.GetService<ISDK>();
sdk.Configure("Pp-ONiK0UvjaX5OyYZcv");

var quit = false;
while (!quit)
{
    Console.Write("Input a command: ");
    var command = Console.ReadLine();
    var stringResponse = String.Empty;

    try
    {
        switch (command.ToLower())
        {
            case "book":
                var bookRequest = new Request<Book>();

                var bookResults = await sdk.GetAll(bookRequest);
                stringResponse = JsonConvert.SerializeObject(bookResults);
                break;
            case "character":
                var charRequest = new Request<Character>()
                    .AddParam("name", "Baggins", ParamType.REGEX);

                var charResults = await sdk.GetAll(charRequest);
                stringResponse = JsonConvert.SerializeObject(charResults);
                break;
            case "chapter":
                var chapterRequest = new Request<Chapter>();

                var chapterResuls = await sdk.GetAll(chapterRequest);
                stringResponse = JsonConvert.SerializeObject(chapterResuls);
                break;
            case "q":
                quit = true;
                continue;
        }

        Console.Write("Results: ");
        Console.WriteLine(stringResponse);
    } 
    catch(Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
