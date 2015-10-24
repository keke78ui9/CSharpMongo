using MongoDB.Driver;
using System;
using System.Configuration;

namespace CSharpMongo
{
    public class Mongo : IMongo
    {
        public Mongo(string connectionName)
        {
            var config = ConfigurationManager.ConnectionStrings[connectionName];
            if (config == null)
            {
                throw new Exception(connectionName + " is required for mongodb database connection.");
            }

            var connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            LoadDatabase(connectionString);
        }

        private void LoadDatabase(string connectionString)
        {
            Url = new MongoUrl(connectionString);
            Client = new MongoClient(Url);
            Database = Client.GetDatabase(Url.DatabaseName);
        }

        public MongoClient Client { get; private set; }
        public IMongoDatabase Database { get; private set; }
        public MongoUrl Url { get; private set; }

        private static string GetCollectionName<T>()
        {
            var type = typeof(T);
            return type.Name;
        }

        public IMongoCollection<T> CollectionName<T>() where T : TDocument
        {
            return Database.GetCollection<T>(GetCollectionName<T>());
        }
    }
}