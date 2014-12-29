using System.Threading.Tasks;
using RestWrap.Interfaces;

namespace RestWrap
{
    public class SaveableRestClient : IRestClient
    {
        private readonly IRestClient _restClient;
        private readonly IResponseSaver _responseSaver;

        public SaveableRestClient(IRestClient restClient, IResponseSaver responseSaver)
        {
            _responseSaver = responseSaver;
            _restClient = restClient;
        }

        public async Task<IResponse> GetResponseAsync(IRequest request)
        {
            var response = await _restClient.GetResponseAsync(request);
            _responseSaver.Save(request.Path, response);
            return response;
        }
    }
}