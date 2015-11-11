using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mongo.Net;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MongoNet net = new MongoNet("");
            var x = net.CollectionName<TestClass>("TestClass");
            
        }
    }

    public class TestClass : TDocument
    {

    }
}
