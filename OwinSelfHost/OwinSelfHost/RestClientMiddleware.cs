using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Owin;
using RestWrap;
using RestWrap.Interfaces;

namespace RestWrapHost
{
    public class RestClientMiddleware : OwinMiddleware
    {
        private readonly IRestClient _client;

        public RestClientMiddleware(OwinMiddleware next, IRestClient client) :
            base(next)
        {
            _client = client;
        }

        public override async Task Invoke(IOwinContext owinContext)
        {
            var request = CreateRequest(owinContext);
            var response = await _client.GetResponseAsync(request);

            CreateResponseHeaders(owinContext, response);
            
            await owinContext.Response.WriteAsync(response.Data);
            await Next.Invoke(owinContext);
        }

        private static void CreateResponseHeaders(IOwinContext owinContext, IResponse response)
        {
            owinContext.Response.Headers.Clear();

            foreach (var header in response.Headers)
            {
                owinContext.Response.Headers.SetValues(header.Key, header.Value);
            }
        }

        private static Request CreateRequest(IOwinContext owinContext)
        {
            var request = new Request
            {
                Path = owinContext.Request.Path.ToString() + owinContext.Request.QueryString,
                Headers = new Dictionary<string, string>()
            };

            foreach (var header in owinContext.Request.Headers)
            {
                if (header.Key == "Host" ||
                    header.Key == "Connection")
                {
                    continue;
                }

                request.Headers.Add(header.Key, header.Value.FirstOrDefault());
            }
            return request;
        }

    }
}
