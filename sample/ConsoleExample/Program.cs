using Mongo.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                AccessMongo();

                TestCsharpMongo();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void TestCsharpMongo()
        {

            var testDb = new MongoNet("test");

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
        /// TODO: if there's no mongodb, console window stop
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