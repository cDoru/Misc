﻿using RestWrap;
using RestWrap.Interfaces;

namespace RestWrapHost
{
    public class RestClientBuilder
    {
        private readonly string _endpoint;
        private IResponseSaver _reponseSaver;
        private IResponseLoader _reponseLoader;
        private int? _millisecondDelay;

        public RestClientBuilder WithSaving(IResponseSaver reponseSaver)
        {
            _reponseSaver = reponseSaver;
            return this;
        }

        public RestClientBuilder WithLoading(IResponseLoader reponseLoader)
        {
            _reponseLoader = reponseLoader;
            return this;
        }

        public RestClientBuilder WithDelay(int millisecondDelay)
        {
            _millisecondDelay = millisecondDelay;
            return this;
        }

        public RestClientBuilder(string endpoint)
        {
            _endpoint = endpoint;                
        }

        public IRestClient Build()
        {
            IRestClient client = new HttpRestClient(_endpoint);

            if (_reponseSaver != null)
            {
                client = new SaveableRestClient(client, _reponseSaver);
            }

            if (_reponseLoader != null)
            {
                client = new LoadableRestClient(client, _reponseLoader);
            }
            
            if (_millisecondDelay != null)
            {
                client = new SlowClient(client, _millisecondDelay.Value);
            }

            return client;
        }

    }
}