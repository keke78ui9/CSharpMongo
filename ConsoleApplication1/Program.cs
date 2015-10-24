using CSharpMongo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                AccessMongo();

                TestCsharpMongo();
            }
            catch (Exception ex)
            {
                // any error
            }
        }

        private static void TestCsharpMongo()
        {
            Mongo testDb = new Mongo("test");

            // add
            testDb.CollectionName<MongoTestCollection>().Insert(new MongoTestCollection
            {
                Name = Guid.NewGuid().ToString(),
                Age = 1
            });

            // get all
            var result = testDb.CollectionName<MongoTestCollection>().FindAll();
            result.FirstOrDefault().Age = 100;

            // count
            testDb.CollectionName<MongoTestCollection>().Count();

            // delete
            testDb.CollectionName<MongoTestCollection>().Delete(x => x.Age == 1);
        }

        /// <summary>
        /// mongo server version: db.version() 3.0.5
        /// </summary>
        private static void AccessMongo()
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("Test");

            var collection = database.GetCollection<BsonDocument>("SampleData");

            var documents = collection.Find(new BsonDocument()).ToListAsync().Result;

            var document = new BsonDocument
            {
                { "name", Guid.NewGuid().ToString() },
                { "age", documents.Count() + 1 }
            };
        }
    }

    public class MongoTestCollection : TDocument
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}