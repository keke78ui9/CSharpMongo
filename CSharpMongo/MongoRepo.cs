using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;

namespace CSharpMongo
{
    public class MongoRepo : IMongoRepository, IMongo
    {
        public MongoRepo(string connectionName)
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

        private string CollectionName<T>()
        {
            Type type = typeof(T);
            return type.Name;
        }

        public long Count<T>() where T : TDocument
        {
            IMongoCollection<T> collections = Database.GetCollection<T>(CollectionName<T>());
            var filter = new BsonDocument();
            return collections.CountAsync(filter).Result;
        }

        public void Add<T>(T t) where T : TDocument
        {
            Database.GetCollection<T>(CollectionName<T>()).InsertOneAsync(t);
        }

        public void Update<T>(Expression<Func<T, bool>> filter, UpdateDefinition<T> update) where T : TDocument
        {
            IMongoCollection<T> collections = Database.GetCollection<T>(CollectionName<T>());
            collections.UpdateOneAsync<T>(filter, update);
        }

        public void Delete<T>(Expression<Func<T, bool>> filter) where T : TDocument
        {
            IMongoCollection<T> collections = Database.GetCollection<T>(CollectionName<T>());
             collections.DeleteManyAsync<T>(filter);
        }

        public List<T> Find<T>() where T : TDocument
        {
            return FindAll<T>();
        }

        private List<T> FindAll<T>()
        {
            IMongoCollection<T> collections = Database.GetCollection<T>(CollectionName<T>());
            return collections.Find(new BsonDocument()).ToListAsync().Result;
        }


        public void Update<T>(T t) where T : TDocument
        {
            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq("Id", t.Id);
            IMongoCollection<T> collections = Database.GetCollection<T>(CollectionName<T>());
            collections.ReplaceOneAsync(filter, t);
        }
    }
}
