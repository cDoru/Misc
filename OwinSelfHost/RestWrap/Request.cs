using System;
using System.Collections.Generic;
using RestWrap.Interfaces;

namespace RestWrap
{
    public class Request : IRequest
    {
        public string Path { get; set; }
        public string Server { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public Uri Uri
        {
            get { return new Uri(Server + Path); }
        }
    }
}