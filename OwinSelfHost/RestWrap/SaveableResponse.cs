using System.Collections.Generic;
using RestWrap.Interfaces;

namespace RestWrap
{
    public class SaveableResponse : IResponse
    {
        private readonly IResponse _response;
        private readonly string _path;

        public SaveableResponse(string path, IResponse response)
        {
            _path = path;
            _response = response;
        }

        public string Path
        {
            get { return _path; }
        }

        public byte[] Data
        {
            get { return _response.Data; }
        }

        public IDictionary<string, string> Headers
        {
            get { return _response.Headers; }
        }
    }
}