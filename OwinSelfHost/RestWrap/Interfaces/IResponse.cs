using System.Collections.Generic;

namespace RestWrap.Interfaces
{
    public interface IResponse
    {
        byte[] Data { get; }
        IDictionary<string, string> Headers { get; }
    }
}
