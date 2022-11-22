using OneAPI.Models.API;
using OneAPI.Models.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAPI.Tests.Unit
{
    
    public class RequestTests
    {
        [TestCase]
        public void ItBuildsParameterlessUrlCorrectly()
        {
            var request = new Request<Book>();

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo(nameof(Book)));
        }

        [TestCase]
        public void ItBuildsUrlWithAscSortParamsCorrectly()
        {
            var request = new Request<Book>()
                .SetSort("name", SortDirection.ASCENDING);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?sort=name:asc"));
        }

        [TestCase]
        public void ItBuildsUrlWithDescSortParamsCorrectly()
        {
            var request = new Request<Book>()
                .SetSort("name", SortDirection.DESCENDING);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?sort=name:desc"));
        }

        [TestCase]
        public void ItBuildsUrlWithEqualsParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "someValue");

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?name=someValue"));
        }

        [TestCase]
        public void ItBuildsUrlWithExcludeParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "someValue", ParamType.EXCLUDE);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?name!=someValue"));
        }

        [TestCase]
        public void ItBuildsUrlWithExistsParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "doesntMatter", ParamType.EXISTS);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?name"));
        }

        [TestCase]
        public void ItBuildsUrlWithGreaterParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "someValue", ParamType.GREATER);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?name>someValue"));
        }

        [TestCase]
        public void ItBuildsUrlWithGTEParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "someValue", ParamType.GTE);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?name>=someValue"));
        }

        [TestCase]
        public void ItBuildsUrlWithIncludeParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "someValue", ParamType.INCLUDE);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?name=someValue"));
        }

        [TestCase]
        public void ItBuildsUrlWithLessParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "someValue", ParamType.LESS);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?name<someValue"));
        }

        [TestCase]
        public void ItBuildsUrlWithLimitCorrectly()
        {
            var request = new Request<Book>()
                .SetLimit(10);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?limit=10"));
        }

        [TestCase]
        public void ItBuildsUrlWithLTEParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "someValue", ParamType.LTE);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?name<=someValue"));
        }

        [TestCase]
        public void ItBuildsUrlWithMultipleParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("_id", "someOtherValue")
                .AddParam("name", "someValue");

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?_id=someOtherValue&name=someValue"));
        }

        [TestCase]
        public void ItBuildsUrlWithNEqualsParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "someValue", ParamType.NEQUALS);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?name!=someValue"));
        }

        [TestCase]
        public void ItBuildsUrlWithNExistsParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "doesntMatter", ParamType.NEXISTS);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?!name"));
        }

        [TestCase]
        public void ItBuildsUrlWithNRegexParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "someValue", ParamType.NREGEX);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?name!=/someValue/i"));
        }

        [TestCase]
        public void ItBuildsUrlWithPageCorrectly()
        {
            var request = new Request<Book>()
                .SetPage(10);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?page=10"));
        }

        [TestCase]
        public void ItBuildsUrlWithRegexParamsCorrectly()
        {
            var request = new Request<Book>()
                .AddParam("name", "someValue", ParamType.REGEX);

            var uri = request.ToString();

            Assert.That(uri, Is.EqualTo($"{nameof(Book)}?name=/someValue/i"));
        }
    }
}
