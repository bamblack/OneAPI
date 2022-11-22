using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAPI.Tests.Lib
{
    internal class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string response;

        public MockHttpMessageHandler(string response)
        {
            this.response = response;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response)
            };
        }
    }
}
