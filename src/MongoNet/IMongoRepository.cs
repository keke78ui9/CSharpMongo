using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoNet
{
    public interface IMongoRepository
    {
        long Count<T>() where T : TDocument;

        void Add<T>(T t) where T : TDocument;

        void Update<T>(T t) where T : TDocument;

        void Update<T>(Expression<Func<T, bool>> filter, UpdateDefinition<T> update) where T : TDocument;

        void Delete<T>(Expression<Func<T, bool>> filter) where T : TDocument;

        List<T> Find<T>() where T : TDocument;
    }
}