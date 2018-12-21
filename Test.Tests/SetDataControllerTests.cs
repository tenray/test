using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Test.Core.DTO;
using Test.Core.Interfaces;
using Test.Web.Controllers;
using Xunit;

namespace Test.Tests
{
    public class SetDataControllerTests
    {
        [Fact]
        public void PostParamNull()
        {
            //Arrange
            var mock = new Mock<IDataService>();
            mock.Setup(repo => repo.GetData()).Returns(GetTestData());            
            SetDataController controller = new SetDataController(mock.Object);

            int[] fakeParam = null;

            //Act
            var result = controller.Post(fakeParam);

            //Assert
            Assert.IsType<JsonResult>(result);     
            Assert.Equal("empty sequence", result.Value);            
        }

        private StorageDTO GetTestData()
        {
            return new StorageDTO() { Values = new List<int> { 1,3,9,2,8} };
        }
    }
}
