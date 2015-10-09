using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using System.Collections;
using System.Linq;
using CSharpMongo;

namespace CSharpMongo
{
    public class MongoRepo : IMongoRepository
    {
        private MongoClient _client;
        private IMongoDatabase _provider;
        private MongoUrl _mongoUrl;

        public Expression Expression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Type ElementType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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
            _mongoUrl = new MongoUrl(connectionString);
            _client = new MongoClient(_mongoUrl);
            _provider = _client.GetDatabase(_mongoUrl.DatabaseName);
        }

        private string CollectionName<Entity>()
        {
            Type type = typeof(Entity);
            return type.Name;
        }

        public long Count<Entity>() where Entity : TDocument
        {
            IMongoCollection<Entity> collections = _provider.GetCollection<Entity>(CollectionName<Entity>());
            var filter = new BsonDocument();
            return collections.CountAsync(filter).Result;
        }

        public void Add<Entity>(Entity entity) where Entity : TDocument
        {
            _provider.GetCollection<Entity>(CollectionName<Entity>()).InsertOneAsync(entity);
        }

        public void Update<Entity>(Expression<Func<Entity, bool>> filter, UpdateDefinition<Entity> update) where Entity : TDocument
        {
            IMongoCollection<Entity> collections = _provider.GetCollection<Entity>(CollectionName<Entity>());
            collections.UpdateOneAsync<Entity>(filter, update);
        }

        public void Delete<Entity>(Expression<Func<Entity, bool>> filter) where Entity : TDocument
        {
            IMongoCollection<Entity> collections = _provider.GetCollection<Entity>(CollectionName<Entity>());
             collections.DeleteManyAsync<Entity>(filter);
        }

        public List<Entity> Find<Entity>() where Entity : TDocument
        {
            return FindAll<Entity>();
        }

        private List<Entity> FindAll<Entity>()
        {
            IMongoCollection<Entity> collections = _provider.GetCollection<Entity>(CollectionName<Entity>());
            return collections.Find(new BsonDocument()).ToListAsync().Result;
        }


        public void Update<Entity>(Entity entity) where Entity : TDocument
        {
            var filterBuilder = Builders<Entity>.Filter;
            var filter = filterBuilder.Eq("Id", entity.Id);
            IMongoCollection<Entity> collections = _provider.GetCollection<Entity>(CollectionName<Entity>());
            collections.ReplaceOneAsync(filter, entity);
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
