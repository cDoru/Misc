using System;
using System.Collections.Generic;

namespace RestWrap.Interfaces
{
    public interface IRequest
    {
        string Path { get; set; }
        string Server { get; set; }
        Dictionary<string, string> Headers { get; set; }
        Uri Uri { get; }
    }
}
