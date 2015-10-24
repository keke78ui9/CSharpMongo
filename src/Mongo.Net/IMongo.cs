using MongoDB.Driver;

namespace CSharpMongo
{
    public interface IMongo
    {
        MongoClient Client { get; }
        IMongoDatabase Database { get; }
        MongoUrl Url { get; }
    }
}