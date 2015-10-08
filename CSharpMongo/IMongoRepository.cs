using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMongo
{
    public interface IMongoRepository
    {
        long Count<Entity>() where Entity : class, new();
        void Add<Entity>(Entity entity) where Entity : class, new();
        void Update<Entity>(Expression<Func<Entity, bool>> filter, UpdateDefinition<Entity> update) where Entity : class, new();
        void Delete<Entity>(Expression<Func<Entity, bool>> filter) where Entity : class, new();
        List<Entity> Find<Entity>() where Entity : class, new();
    }
}
