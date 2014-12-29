using System.IO;
using System.Net;
using System.Threading.Tasks;
using RestWrap.Interfaces;

namespace RestWrap
{
    public class HttpRestClient : IRestClient
    {
        private readonly string _server;

        public HttpRestClient(string server)
        {
            _server = server;
        }

        public async Task<IResponse> GetResponseAsync(IRequest request)
        {
            request.Server = _server;

            var webRequest = (HttpWebRequest)WebRequest.Create(request.Uri);

            webRequest.Method = "GET";
             
            foreach (var pair in request.Headers)
            {
                if (pair.Key == "Accept")
                {
                    webRequest.Accept = "application/json";
                }
                else
                {
                    webRequest.Headers.Add(pair.Key, pair.Value);
                }
            }

            var output = new Response();

            using (var response = (HttpWebResponse)await webRequest.GetResponseAsync())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        output.Data = ReadBytes(responseStream);
                    }
                }

                foreach (var header in response.Headers.AllKeys)
                {
                    output.Headers.Add(header, response.Headers[header]);
                }
            }

            return output;
        }

        private static byte[] ReadBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}