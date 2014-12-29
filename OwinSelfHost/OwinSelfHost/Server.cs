using System;
using Microsoft.Owin.Hosting;
using Owin;
using RestWrap;
using RestWrap.Interfaces;

namespace RestWrapHost
{
    public class Server : IDisposable
    {
        private IDisposable _webApp;

        private readonly string _baseUrl;
        private readonly IRestClient _restClient;

        public Server(string baseUrl, IRestClient restClient)
        {
            _restClient = restClient;
            _baseUrl = baseUrl;
            Start();
        }

        private void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use(typeof(RestClientMiddleware), _restClient);
        }

        private void Start()
        {
            _webApp = WebApp.Start(_baseUrl, Configuration);
        }

        public void Dispose()
        {
            _webApp.Dispose();
        }
    }

    public class OwinServer : IDisposable
    {
        private IDisposable _webApp;

        private readonly string _baseUrl;
        private readonly IRestClient[] _restClients;

        public OwinServer(string baseUrl, IRestClient[] restClients)
        {
            _restClients = restClients;
            _baseUrl = baseUrl;
            Start();
        }

        private void Configuration(IAppBuilder appBuilder)
        {
            foreach (var restClient in _restClients)
            {
                appBuilder.Use(typeof (RestClientMiddleware), restClient);
            }
        }

        private void Start()
        {
            _webApp = WebApp.Start(_baseUrl, Configuration);
        }

        public void Dispose()
        {
            _webApp.Dispose();
        }
    }
}
