using System.Collections.Generic;
using System.Configuration;
using RestWrap.Interfaces;
using RestWrapHost;


namespace RestWrap.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            const string buildUrl = "http://localhost:5001/";

            var builder = new RestClientBuilder("http://api.marksandspencer.com");
            
            var fileStorage = new FileStorage(@"C:\Users\Daniel\Documents\Visual Studio 2013\Projects\OwinSelfHost\Cache\");

            var inMemoryStorage = new InMemoryStorage();

            var client = builder
                .WithLoading(inMemoryStorage)
                .WithSaving(inMemoryStorage)
                .Build();

            //builder.Splitter.Routes(
            //    r => r.Logic(p => p.Contains("product")).Route(r => r.CacheToFile("fdff").EndPoint("fdsfdf"))
            //    );

            var logger = new RestClientLogger(client);

            using (var server = new Server(buildUrl, logger))
            {
                System.Console.WriteLine("Press any key to close");
                System.Console.ReadKey();
            }
        }
    }
}
