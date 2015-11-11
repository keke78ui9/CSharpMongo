using MongoDB.Driver;

namespace MongoNet
{
    public interface IMongo
    {
        MongoClient Client { get; }
        IMongoDatabase Database { get; }
        MongoUrl Url { get; }
    }
}