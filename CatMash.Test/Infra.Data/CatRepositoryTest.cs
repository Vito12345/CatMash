using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CatMash.Common.Config;
using CatMash.Domain;
using CatMash.Domain.Configuration;
using CatMash.Infra.Data;
using CatMash.Infra.Data.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace CatMash.Test.Infra.Data
{
    [TestClass]
    public class CatRepositoryTest
    {
        private ICatsRepository _sut;
        private Mock<IConfigRepo<CatMashConfig>> _mockConfigRepo;
        private CatMashConfig _catMashConfig = new CatMashConfig();
        private Mock<IFileParser> _mockFileParser;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockConfigRepo = new Mock<IConfigRepo<CatMashConfig>>();
            _mockConfigRepo.Setup(x => x.Configuration).Returns(_catMashConfig);

            _mockFileParser = new Mock<IFileParser>();
            _sut = new CatsRepository(_mockFileParser.Object, _mockConfigRepo.Object);
        }

        [TestMethod]
        [TestCategory("OnBuild")]
        public async Task CatRepositoryTest_GetAllCatsAsync_FileNotFound()
        {
            // arrange
            _mockFileParser.Setup(x => x.ReadFileAsync(It.IsAny<string>())).Throws(new IOException());

            // act
            var actual = await _sut.GetAllCatsAsync();

            // assert
            Assert.AreEqual(0, actual.Count());
            //mockLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [TestCategory("OnBuild")]
        public async Task CatRepositoryTest_GetAllCatsAsync_MalformedJson()
        {
            // arrange
            _mockFileParser.Setup(x => x.ReadFileAsync(It.IsAny<string>())).Returns(Task.FromResult("The cake is a lie."));

            // act
            var actual = await _sut.GetAllCatsAsync();

            // assert
            Assert.AreEqual(0, actual.Count());
            //mockLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [TestCategory("OnBuild")]
        public async Task CatRepositoryTest_GetAllCatsAsync_EmptyJson()
        {
            // arrange
            _mockFileParser.Setup(x => x.ReadFileAsync(It.IsAny<string>())).Returns(Task.FromResult(string.Empty));

            // act
            var actual = await _sut.GetAllCatsAsync();

            // assert
            Assert.AreEqual(0, actual.Count());
            //mockLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("OnBuild")]
        public async Task CatRepositoryTest_GetAllCatsAsync_OK()
        {
            // arrange
            _mockFileParser
                .Setup(x => x.ReadFileAsync(It.IsAny<string>()))
                .Returns(
                    Task.FromResult(
                        JsonConvert.SerializeObject(
                            new List<Cat> {
                                new Cat { Id = "aa", Url = "bb", Votes = 1 }
                            }
                        )
                    )
                );

            // act
            var actual = await _sut.GetAllCatsAsync();

            // assert
            Assert.AreEqual(0, actual.Count());
            //mockLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Once);
        }
    }
}
