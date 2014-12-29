using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RestWrap.Interfaces;

namespace RestWrap
{
    public class FileStorage : IResponseSaver, IResponseLoader
    {
        private readonly string _directory;

        public FileStorage(string directory)
        {
            _directory = directory;
        }

        public void Save(string path, IResponse response)
        {
            var saveableResponse = new SaveableResponse(path, response);
            var str = JsonConvert.SerializeObject(saveableResponse);
            File.WriteAllText(_directory + Guid.NewGuid(), str);
        }

        public IResponse Load(string path)
        {
            return Directory.GetFiles(_directory)
                .Select(File.ReadAllText)
                .Select(JsonConvert.DeserializeObject<LoadableResponse>)
                .FirstOrDefault(loadableResponse => loadableResponse.Path == path);
        }
    }
}