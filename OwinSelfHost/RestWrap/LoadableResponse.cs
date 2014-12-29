using System.Collections.Generic;
using RestWrap.Interfaces;

namespace RestWrap
{
    public class LoadableResponse : IResponse
    {
        private LoadableResponse() {}

        public string Path { get; set; }

        public byte[] Data { get; set; }

        public IDictionary<string, string> Headers { get; set; }

    }
}