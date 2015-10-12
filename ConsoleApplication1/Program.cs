using CSharpMongo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
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
            Mongo db = new Mongo("test");
            
            // add stuff
            db.Add<MongoTestCollection>(new MongoTestCollection 
            {
            Name = Guid.NewGuid().ToString(),
            Age = 1
            });

            // get
            var result = db.Find<MongoTestCollection>();

            // update
            result.FirstOrDefault().Age = 100;
            db.Update<MongoTestCollection>(result.FirstOrDefault());

            // count
            var count = db.Count<MongoTestCollection>();

            // delete
            db.Delete<MongoTestCollection>(x => x.Age == 1);

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
