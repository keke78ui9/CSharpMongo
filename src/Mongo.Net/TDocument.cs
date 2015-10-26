using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongo.Net
{
    public class TDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }
}