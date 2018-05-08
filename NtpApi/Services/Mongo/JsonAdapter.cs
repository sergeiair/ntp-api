using System.Collections.Generic;
using System.IO;
using MongoDB.Bson;
using Newtonsoft.Json;
using NtpApi.Models;

namespace NtpApi.Services.Mongo
{
    public class JsonAdapter
    {
        private readonly string _filePath;
        
        public JsonAdapter(string dir = null)
        {
            if (dir == null)
            {
                _filePath = Directory.GetCurrentDirectory() + "\\Json\\";
            }
        }

        
        public string getPreparedForInsert(string fileName)
        {
            using (var file = File.OpenText(_filePath + fileName))
            {
                var serializer = new JsonSerializer();
                var fixtures = (IEnumerable<SerializationFixture>) serializer.Deserialize(
                    file, typeof(IEnumerable<SerializationFixture>)
                );

                return fixtures.ToJson();
            }
        }
    }
}