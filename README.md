# CSharpMongo
A MongoLibrary for C#
+ use MongoDriver 2.0.1
+ test MongoDB Server 3.0.5

###### Sample code

Connection String
```
  <connectionStrings>
    <add name="test" connectionString="mongodb://localhost:27017/Test"/>
  </connectionStrings>
```
Define Class, Document
```
    public class MongoTestCollection : Entity
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
```

Example code for CRUD
```
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
```
