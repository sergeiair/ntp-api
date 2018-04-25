using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NtpApi.Models
{
    public class Country {
        [BsonId]
        public ObjectId Id { get; set; }
        public int Id_country { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }
}
