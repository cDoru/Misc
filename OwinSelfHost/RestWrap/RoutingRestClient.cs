using System;
using System.Threading.Tasks;
using RestWrap.Interfaces;

namespace RestWrap
{
    public class RoutingRestClient : IRestClient
    {
        public Func<string, IRestClient> RoutingAction { get; set; }

        public IRestClient DefaultRestClient { get; set; }

        public async Task<IResponse> GetResponseAsync(IRequest request)
        {
            if (RoutingAction != null)
            {
                var client = RoutingAction(request.Path);

                if (client != null)
                {
                    return await client.GetResponseAsync(request);
                }
            }

            return await DefaultRestClient.GetResponseAsync(request);
        }
    }
}