using OneAPI.Models.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneAPI.Contracts
{
    public interface ISDK
    {
        void Configure(string authKey);
        /// <summary>
        /// Execute a request against the API. Use to implement your own pagination
        /// </summary>
        Task<List<T>> Get<T>(Request<T> request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Execute a request against the API, returning all results (will continuously request batches of data until all data is retrieved). Use if you don't care about pagination
        /// </summary>
        Task<List<T>> GetAll<T>(Request<T> request, CancellationToken cancellationToken = default);
    }
}
