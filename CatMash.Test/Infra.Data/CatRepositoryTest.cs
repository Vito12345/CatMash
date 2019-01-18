using System.Threading.Tasks;
using CatMash.Infra.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CatMash.Test.Infra.Data
{
    [TestClass]
    public class CatRepositoryTest
    {
        private ICatsRepository _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new CatsRepository();
        }

        [TestMethod]
        [TestCategory("OnBuild")]
        public async Task CatRepositoryTest_GetAllCatsAsync_FileNotFound()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("OnBuild")]
        public async Task CatRepositoryTest_GetAllCatsAsync_MalformedJson()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("OnBuild")]
        public async Task CatRepositoryTest_GetAllCatsAsync_EmptyJson()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("OnBuild")]
        public async Task CatRepositoryTest_GetAllCatsAsync_OK()
        {
            Assert.Fail();
        }
    }
}
