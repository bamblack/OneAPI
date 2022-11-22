using NSubstitute;
using NSubstitute.Extensions;
using OneAPI.Contracts;
using OneAPI.Models.API;
using OneAPI.Models.Lib;
using OneAPI.Services;
using OneAPI.Tests.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAPI.Tests.Unit
{
    public class GivenANewSDK
    {
        protected readonly IHttpClientFactory httpClientFactory;
        protected readonly ISDK sdk;

        public GivenANewSDK()
        {
            this.httpClientFactory = Substitute.For<IHttpClientFactory>();

            this.sdk = new SDK(httpClientFactory);
        }

        [SetUp]
        public void Setup()
        {
            var mockMessageHandler = new MockHttpMessageHandler("{ docs: [{}, {}, {}, {}, {}], total: 100 }");
            var mockHttpClient = new HttpClient(mockMessageHandler);
            this.httpClientFactory.CreateClient().ReturnsForAnyArgs(mockHttpClient);
        }

        public class WhenMakingAGetRequest : GivenANewSDK
        {

            [TestCase]
            public async Task ItProcessesCorrectly()
            {
                var request = new Request<Book>();
                var results = await this.sdk.Get(request);

                this.httpClientFactory.Received(1).CreateClient();
                Assert.That(results.Count, Is.EqualTo(5));
            }
        }

        public class WhenMakingAGetAllRequest : GivenANewSDK
        {
            [TestCase]
            public async Task ItProcessesCorrectly()
            {
                var request = new Request<Book>();
                var results = await this.sdk.GetAll(request);

                Assert.That(results.Count, Is.EqualTo(100));
            }
        }
    }
}
