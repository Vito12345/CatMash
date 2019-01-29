using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatMash.Business;
using CatMash.Domain;
using CatMash.Infra.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CatMash.Test.Business
{
    [TestClass]
    public class CatManagerTest
    {
        private ICatManager _sut;
        private Mock<ICatsRepository> _mockCatRepo;

        [TestInitialize]
        public void TestInit()
        {
            _mockCatRepo = new Mock<ICatsRepository>();
            _mockCatRepo.Setup(x => x.GetAllCatsAsync()).Returns(Task.FromResult<IEnumerable<Cat>>(new List<Cat>()));
            _sut = new CatManager(_mockCatRepo.Object);
        }

        [TestMethod]
        [TestCategory("OnBuild")]
        public async void CatManagerTest_GetAllCats_NoData()
        {
            // arrange
            _mockCatRepo.Setup(x => x.GetAllCatsAsync()).Returns(Task.FromResult<IEnumerable<Cat>>(null));

            // act
            var actual = await _sut.GetAllCats();

            // assert
            Assert.IsNotNull(actual, "In case of no data, the returned list should be an empty list.");
            Assert.IsTrue(!actual.Any(), "In case of no data, the returned list should be an empty list.");
        }

        [TestMethod]
        [TestCategory("OnBuild")]
        public async void CatManagerTest_GetAllCats_OK()
        {
            // arrange
            var data = new List<Cat> {
                        new Cat { Id = "aa", Url = "http://aaa", Votes = 10 },
                        new Cat { Id = "bb", Url = "http://bbb", Votes = 1 }
                    };
            _mockCatRepo
                .Setup(x => x.GetAllCatsAsync())
                .Returns(Task.FromResult<IEnumerable<Cat>>(data));

            // act
            var actual = await _sut.GetAllCats();

            // assert
            Assert.IsNotNull(actual, "The returned list should not be null.");
            Assert.AreEqual(data.Count(), actual.Count());
        }

        [TestMethod]
        [TestCategory("OnBuild")]
        public async void CatManagerTest_GetAllCats_Empty()
        {
            // arrange
            _mockCatRepo
                .Setup(x => x.GetAllCatsAsync())
                .Returns(Task.FromResult<IEnumerable<Cat>>(new List<Cat>()));

            // act
            var actual = await _sut.GetAllCats();

            // assert
            Assert.IsNotNull(actual, "The returned list should not be null.");
            Assert.IsTrue(!actual.Any());
        }
    }
}
