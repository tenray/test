using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Test.Core.Interfaces;
using Test.Core.SortLogic;
using Test.Web.Controllers;
using Xunit;

namespace Test.Tests
{
    public class BubbleSortControllerTests
    {
        [Fact]
        public void BubbleSortLengthGreatThenZero()
        {
            //Arrange
            var dataMock = new Mock<IDataService>();           
            dataMock.Setup(repo => repo.GetData()).Returns(new Core.DTO.StorageDTO());          
            BubbleSortController controller = new BubbleSortController(dataMock.Object, new BubbleSorting(dataMock.Object));
            
            //Act
            var result = controller.Get(0);//as ObjectResult;            

            //Assert          
            Assert.IsType<ObjectResult>(result);  
        }

        [Fact]
        public void BubbleSortLengthNotNull()
        {
            //Arrange
            var dataMock = new Mock<IDataService>();
            dataMock.Setup(repo => repo.GetData()).Returns(new Core.DTO.StorageDTO() { Values = GetFakeValues() });
            BubbleSortController controller = new BubbleSortController(dataMock.Object, new BubbleSorting(dataMock.Object));
            
            //Act
            var result = controller.Get(11);//as ObjectResult;
            
            //Assert
            Assert.IsType<JsonResult>(result);           
        }

        private List<int> GetFakeValues()
        {
            return new List<int> { 1, 3, 9, 2, 8 };
        }
    }
}
