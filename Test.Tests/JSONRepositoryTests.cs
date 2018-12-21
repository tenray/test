using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Test.Data.Repositories;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace Test.Tests
{
    public class JSONRepositoryTests
    {
        [Fact]
        public void ReadWriteTest()
        {
            //Arrange
            JsonRepository db = new JsonRepository();

            //Act
            db.Write(GetFakeValues().ToArray());
            var result = db.Read().OrderBy(x => x).ToList();
           
            //Assert 
            var expected = GetFakeValues().OrderBy(x => x).ToList();

            Assert.True(!expected.Except(result).Any() && expected.Count == result.Count);
        }


        private List<int> GetFakeValues()
        {
            return new List<int> { 1, 3, 9, 2, 8, 17, 14, 2 };
        }
    }
}
