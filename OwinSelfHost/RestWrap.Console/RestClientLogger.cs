using System.Diagnostics;
using System.Threading.Tasks;
using RestWrap.Interfaces;

namespace RestWrap.Console
{
    public class RestClientLogger : IRestClient
    {
        private readonly IRestClient _restClient;

        public RestClientLogger(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IResponse> GetResponseAsync(IRequest request)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
    
            var reponse = await _restClient.GetResponseAsync(request);

            System.Console.WriteLine("{0} {1}ms", request.Path, stopwatch.ElapsedMilliseconds);

            return reponse;
        }
    }
}