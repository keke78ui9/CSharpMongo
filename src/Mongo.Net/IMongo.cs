using MongoDB.Driver;

namespace Mongo.Net
{
    public interface IMongo
    {
        MongoClient Client { get; }
        IMongoDatabase Database { get; }
        MongoUrl Url { get; }
    }
}