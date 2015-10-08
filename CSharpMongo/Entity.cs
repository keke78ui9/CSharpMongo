using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpMongo
{
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }
}
