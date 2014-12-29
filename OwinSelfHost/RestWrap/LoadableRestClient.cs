using System.Threading.Tasks;
using RestWrap.Interfaces;

namespace RestWrap
{
    public class LoadableRestClient : IRestClient
    {
        private readonly IRestClient _restClient;

        private readonly IResponseLoader _responseLoader;

        public LoadableRestClient(IRestClient restClient, IResponseLoader responseLoader)
        {
            _responseLoader = responseLoader;
            _restClient = restClient;
        }

        public async Task<IResponse> GetResponseAsync(IRequest request)
        {
            var response = _responseLoader.Load(request.Path);

            if (response != null)
            {
                return response;
            }

            return await _restClient.GetResponseAsync(request);
        }
    }
}