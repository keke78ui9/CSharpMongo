using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;

namespace CSharpMongo
{
    public interface IMongo
    {
        MongoClient Client { get; }
        IMongoDatabase Database { get; }
        MongoUrl Url { get; }
    }
}
