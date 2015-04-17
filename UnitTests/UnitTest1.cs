using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Javascripting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Test t = new Test();
            Assert.AreEqual(true, t.TestMethod());
        }
    }
}
