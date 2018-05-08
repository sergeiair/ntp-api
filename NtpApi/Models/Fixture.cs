using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NtpApi.Models
{
    public class Fixture : SerializationFixture {
        
        [BsonId]
        public ObjectId Id { get; set; }

    }
}
