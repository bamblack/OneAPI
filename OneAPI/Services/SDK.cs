using Newtonsoft.Json;
using OneAPI.SDK.Contracts;
using OneAPI.SDK.Models.Lib;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace OneAPI.SDK.Services
{
    public class TheOneService : ITheOneService
    {
        private const string baseUri = "https://the-one-api.dev/v2/";
        private string authKey;
        private readonly IHttpClientFactory httpClientFactory;

        public TheOneService
        (
            IHttpClientFactory httpClientFactory
        )
        {
            this.httpClientFactory = httpClientFactory;
        }

        public void Configure(string authKey)
        {
            this.authKey = authKey;
        }

        /// <summary>
        /// Execute a request against the API. Use to implement your own pagination
        /// </summary>
        public async Task<List<T>> Get<T>(Request<T> request, CancellationToken cancellationToken = default)
        {
            var response = await this.Get<T>(request.ToString(), cancellationToken);
            var results = new List<T>(response.docs);
            return results;
        }

        /// <summary>
        /// Execute a request against the API, returning all results (will continuously request batches of data until all data is retrieved). Use if you don't care about pagination
        /// </summary>
        public async Task<List<T>> GetAll<T>(Request<T> request, CancellationToken cancellationToken = default)
        {
            var pageNo = 1;
            request.SetPage(pageNo);

            var response = await this.Get<T>(request.ToString(), cancellationToken);
            var results = new List<T>(response.docs);

            while (response.total > results.Count)
            {
                request.SetPage(++pageNo);
                response = await this.Get<T>(request.ToString(), cancellationToken);
                results.AddRange(response.docs);
            }

            return results;
        }

        
        private async Task<Response<T>> Get<T>(string uri, CancellationToken cancellationToken = default)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authKey);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetStringAsync(new Uri($"{baseUri}{uri}"), cancellationToken);
            return JsonConvert.DeserializeObject<Response<T>>(response);
        }
    }
}
