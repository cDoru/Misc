using NUnit.Framework;
using OwinSelfHost.Integration.Test.Helpers;

namespace RestWrap.Integration.Test
{
    public class RestClientTests
    {
        [Test]
        public void HttpRestClientRequest()
        {
            var sut = new HttpRestClient("https://www.google.com");

            var request = TestMother.BuildRequest();

            var response = sut.GetResponseAsync(request).Result;
           
            Assert.IsTrue(response.Data.Length > 0);
        }

        [Test]
        public void RoutingRestClientRequest()
        {
            var sut = new RoutingRestClient
            {
                DefaultRestClient = new HttpRestClient("https://www.google.com")
            };
            //sut.RoutingAction = p => sut.DefaultRestClient;

            var request = TestMother.BuildRequest();

            var response = sut.GetResponseAsync(request).Result;

            Assert.IsTrue(response.Data.Length > 0);
        }
    }
}