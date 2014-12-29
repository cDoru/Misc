using System.Threading.Tasks;
using RestWrap.Interfaces;

namespace RestWrap
{
    public class SlowClient : IRestClient
    {
        private readonly IRestClient _restClient;
        private readonly int _milliseconds;

        public SlowClient(IRestClient restClient, int milliseconds)
        {
            _milliseconds = milliseconds;
            _restClient = restClient;
        }

        public async Task<IResponse> GetResponseAsync(IRequest request)
        {
            await Task.Delay(_milliseconds);
            return await _restClient.GetResponseAsync(request);
        }
    }
}