using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMongo
{
    public interface IMongoRepository: IQueryable
    {
        long Count<Entity>() where Entity : TDocument;
        void Add<Entity>(Entity entity) where Entity : TDocument;
        void Update<Entity>(Entity entity) where Entity : TDocument;
        void Update<Entity>(Expression<Func<Entity, bool>> filter, UpdateDefinition<Entity> update) where Entity : TDocument;
        void Delete<Entity>(Expression<Func<Entity, bool>> filter) where Entity : TDocument;
        List<Entity> Find<Entity>() where Entity : TDocument;
    }
}
