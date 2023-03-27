using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using ProjectApi.Controllers;
using ProjectApi.DTOs;
using ProjectApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApiTest
{
    [TestFixture]
    public class SkillsControllerTest
    {

        private Mock<IConfiguration> _configurationMock;
        private Mock<MongoClient> _dbClientMock;
        private SkiilsController _controller;

        [SetUp]
        public void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _dbClientMock = new Mock<MongoClient>();
            _controller = new SkiilsController(_configurationMock.Object, _dbClientMock.Object);
        }
        [Test]
        public async Task Skills_Returns_Cached_Result_If_Available()
        {
            // Arrange
            var expectedResults = new List<SkillsDTO>()
        {
            new SkillsDTO() { Skill= "Skill 1" },
            new SkillsDTO() { Skill = "Skill 2" }
        };
            //_controller.Cache.Set("SkillsList", expectedResults);

            // Act
            var result = await _controller.Skills();

            // Assert
            ActionResult<SkillsDTO> resu = _controller.Skills().Result;
            var jsonResult = Assert.IsInstanceOf<JsonResult<SkillsDTO>>(resu);
            var actualResults = Assert.IsInstanceOf<List<SkillsDTO>>(jsonResult.Value);
            Assert.AreEqual(expectedResults.Count, actualResults.Count);
            for (int i = 0; i < expectedResults.Count; i++)
            {
                Assert.AreEqual(expectedResults[i].Skill, actualResults[i].Name);
            }
        }

        [Test]
        public async Task Skills_Returns_Result_From_Database_If_Not_Cached()
        {
            // Arrange
            var dbCollectionMock = new Mock<IMongoCollection<Skills>>();
            var dbList = new List<Skills>()
        {
            new Skills() { Skill= "Skill 1" },
            new Skills() { Skill = "Skill 2" }
        };
            dbCollectionMock.Setup(x => x.AsQueryable()).Returns(dbList.AsQueryable());
            _dbClientMock.Setup(x => x.GetDatabase("Portfolio")).Returns(Mock.Of<IMongoDatabase>(x => x.GetCollection<Skills>("Skills") == dbCollectionMock.Object));

            // Act
            var result = await _controller.Skills();

            // Assert
            var jsonResult = Assert.IsInstanceOf<JsonResult>(result);
            var actualResults = Assert.IsInstanceOf<List<SkillsDTO>>(jsonResult.Value);
            Assert.AreEqual(dbList.Count, actualResults.Count);
            for (int i = 0; i < dbList.Count; i++)
            {
                Assert.AreEqual(dbList[i].Skill, actualResults[i].Name);
            }
        }

        [Test]
        public async Task Skills_Returns_Error_If_Database_Returns_Null()
        {
            // Arrange
            _dbClientMock.Setup(x => x.GetDatabase("Portfolio")).Returns(Mock.Of<IMongoDatabase>(x => x.GetCollection<Skills>("Skills") == null));

            // Act
            var result = await _controller.Skills();

            // Assert
            var jsonResult = Assert.IsInstanceOf<JsonResult>(result);
            var actualResult = Assert.IsInstanceOf<string>(jsonResult.Value);
            Assert.AreEqual("There is some error", actualResult);
        }

        [Test]
        public async Task Skills_Returns_Error_If_Exception_Is_Thrown()
        {
            // Arrange
            _dbClientMock.Setup(x => x.GetDatabase("Portfolio")).Throws(new Exception("Test exception"));

            // Act
            var result = await _controller.Skills();

            // Assert
            var jsonResult = Assert.IsInstanceOf<JsonResult>(result);
            var actualResult = Assert.IsInstanceOf<string>(jsonResult.Value);
            Assert.AreEqual("Test exception", actualResult);
        }
    }
}
