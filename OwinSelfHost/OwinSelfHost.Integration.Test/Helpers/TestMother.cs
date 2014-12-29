using System.Collections.Generic;
using RestWrap;
using RestWrap.Interfaces;

namespace OwinSelfHost.Integration.Test.Helpers
{
    public static class TestMother
    {
        public static IRequest BuildRequest()
        {
            var request = new Request
            {
                Path = "/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=hello+world",
                Server = "https://www.google.com",
                Headers = new Dictionary<string, string>()
                //Headers = new Dictionary<string, string>
                //{
                //    {"Accept", "application/json"},
                //    {"Authorization", string.Format("MSAuth apikey={0}", "apikey")}
                //}
            };
            return request;
        }

    }
}
