using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace DumbSearch.Service.Tests
{
    [TestClass]
    public class SearchTests
    {
        [TestMethod]
        public void ctor_Test()
        {
            var s = new DumbSearch.Service.Searcher();

            Assert.IsNotNull(s);

            Assert.IsNotNull(s.Explorer);
            Assert.IsInstanceOfType(s.Explorer, typeof(DumbSearch.Service.Explorer));

         }

        [TestMethod]
        public void Search_Wildcard()
        {
            var foundFiles = new string[] { "z:\\abc\\def.exe" };
            var wildcard = "*.exe";

            var s = new DumbSearch.Service.Searcher();

            var m = new Moq.Mock<DumbSearch.Service.IExplorer>(MockBehavior.Strict);
            m.Setup(e => e.Explore(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), wildcard))
                .Returns(new List<string>(foundFiles));

            s.Explorer = m.Object;
            var expected = new List<string>(foundFiles);
            var actual = s.Search(wildcard);

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);

            m.VerifyAll();
        }

    }
}
