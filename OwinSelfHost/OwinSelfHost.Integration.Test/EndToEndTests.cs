using NUnit.Framework;
using OwinSelfHost.Integration.Test.Helpers;
using RestWrapHost;

namespace RestWrap.Integration.Test
{
    public class EndToEndTests
    {
        [Test]
        public void EndToEndTest()
        {
            const string buildUrl = "http://localhost:5000/";

            byte[] expected;
            byte[] actual;

            var restClient = new HttpRestClient("https://www.google.com");


            var routing = new RoutingRestClient
            {
                DefaultRestClient = restClient,
                RoutingAction = null
            };

            using (var service = new Server(buildUrl, routing))
            {
                var request = TestMother.BuildRequest();
                var response = restClient.GetResponseAsync(request).Result;

                expected = response.Data;

                request = TestMother.BuildRequest();
                request.Server = buildUrl;
                response = restClient.GetResponseAsync(request).Result;

                actual = response.Data;
            }

            Assert.IsNotNull(expected);
            Assert.IsTrue(expected.Length > 100);
        }
    }
}
