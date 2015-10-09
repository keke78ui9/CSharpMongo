﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpMongo
{
    public class TDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }
}
