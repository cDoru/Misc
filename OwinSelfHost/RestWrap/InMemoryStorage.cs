using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RestWrap.Interfaces;

namespace RestWrap
{
    public class InMemoryStorage : IResponseSaver, IResponseLoader
    {
        private readonly Dictionary<string, IResponse> _dictionary;

        public InMemoryStorage()
        {
            _dictionary = new Dictionary<string, IResponse>();
        }

        public void Save(string path, IResponse response)
        {
            _dictionary[path] = response;
        }

        public IResponse Load(string path)
        {
            if (_dictionary.ContainsKey(path))
            {
                return _dictionary[path];
            }
            return null;
        }
    }
}