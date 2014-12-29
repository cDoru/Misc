using System.Collections.Generic;
using System.IO;
using RestWrap.Interfaces;

namespace RestWrap
{
    public class Response : IResponse
    {
        public byte[] Data { get; set; }
        public IDictionary<string, string> Headers { get; private set; }

        public Response()
        {
            Headers = new Dictionary<string, string>();
        }
    }
}