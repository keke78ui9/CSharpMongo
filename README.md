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

Get Result
```
MongoRepo repo = new MongoRepo("test");
            
// add stuff
repo.Add<MongoTestCollection>(new MongoTestCollection 
{
    Name = Guid.NewGuid().ToString(),
    Age = 1
});

// get stuff
var result = repo.Find<MongoTestCollection>();

// count stuff
var count = repo.Count<MongoTestCollection>();

// delete stuff
repo.Delete<MongoTestCollection>(x => x.Age == 1);
```
