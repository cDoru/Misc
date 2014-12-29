using System.Threading.Tasks;

namespace RestWrap.Interfaces
{
    public interface IRestClient
    {
        Task<IResponse> GetResponseAsync(IRequest request);
    }
}
