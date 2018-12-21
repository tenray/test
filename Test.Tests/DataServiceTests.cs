using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Core.DTO;
using Test.Core.Infrastructure;
using Test.Core.Interfaces;
using Test.Core.Services;
using Test.Data.Interfaces;
using Xunit;

namespace Test.Tests
{
    public class DataServiceTests
    {
        [Fact]
        public void GetDataNull()
        {
            //Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetValues()).Returns(GetDataNullValue());
            DataService ds = new DataService(mock.Object);
                       
            //Assert
            var exception = Assert.Throws<ValidationException>(() => ds.GetData());
            Assert.Equal("storage is empty", exception.Message);            
        }

        [Fact]
        public void GetDataNotNull()
        {
            //Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetValues()).Returns(GetFakeValues());
            DataService ds = new DataService(mock.Object);

            //Act
            var result = ds.GetData();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Values);
            Assert.IsType<StorageDTO>(result);
        }

        [Fact]
        public void SetDataDTONull()
        {
            //Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetValues()).Returns(GetFakeValues());
            DataService ds = new DataService(mock.Object);

            //Act
            StorageDTO storage = null;

            //Assert
            var exception = Assert.Throws<ValidationException>(() => ds.SetData(storage));
            Assert.Equal("DTO null", exception.Message);
        }

        [Fact]
        public void SetDataDTOValuesNull()
        {
            //Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetValues()).Returns(GetFakeValues());
            DataService ds = new DataService(mock.Object);

            //Act
            StorageDTO storage = new StorageDTO();

            //Assert
            var exception = Assert.Throws<ValidationException>(() => ds.SetData(storage));
            Assert.Equal("DTO values null", exception.Message);
        }

        private List<int> GetDataNullValue()
        {
            return null;
        }

        private List<int> GetFakeValues()
        {
            return new List<int> { 1, 3, 9, 2, 8 };
        }

        private StorageDTO GetDataTest()
        {
            return new StorageDTO() { Values = new List<int> { 1, 3, 9, 2, 8 } };
        }
    }
}
