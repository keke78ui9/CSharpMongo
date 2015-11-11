using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoNet;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MongoDabase net = new MongoDabase("");
            var x = net.CollectionName<TestClass>("TestClass");
            
        }
    }

    public class TestClass : TDocument
    {

    }
}
