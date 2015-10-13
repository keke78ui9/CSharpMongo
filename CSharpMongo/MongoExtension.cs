using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CSharpMongo
{
    public static class MongoExtension
    {
        public static long Count<T>(this IMongoCollection<T> collection) where T : TDocument
        {
            var filter = new BsonDocument();
            return collection.CountAsync(filter).Result;
        }

        public static IMongoCollection<T> Insert<T>(this IMongoCollection<T> collection, T t) where T : TDocument
        {
            collection.InsertOneAsync(t);
            return collection;
        }

        public static IMongoCollection<T> Update<T>(this IMongoCollection<T> collection, Expression<Func<T, bool>> filter, UpdateDefinition<T> update) where T : TDocument
        {
            collection.UpdateOneAsync<T>(filter, update);
            return collection;
        }

        public static IMongoCollection<T> Delete<T>(this IMongoCollection<T> collection, Expression<Func<T, bool>> filter) where T : TDocument
        {
            collection.DeleteManyAsync<T>(filter);
            return collection;
        }

        public static List<T> FindAll<T>(this IMongoCollection<T> collection)
        {
            return collection.Find(new BsonDocument()).ToListAsync().Result;
        }

        public static IMongoCollection<T> Update<T>(this IMongoCollection<T> collection, T t) where T : TDocument
        {
            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq("Id", t.Id);
            collection.ReplaceOneAsync(filter, t);
            return collection;
        }
    }
}
