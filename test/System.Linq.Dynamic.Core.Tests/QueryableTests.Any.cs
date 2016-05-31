﻿using System.Linq.Dynamic.Core.Tests.Helpers.Models;
using Xunit;

namespace System.Linq.Dynamic.Core.Tests
{
    public partial class QueryableTests
    {
        [Fact]
        public void Any()
        {
            //Arrange
            IQueryable testListFull = User.GenerateSampleModels(100).AsQueryable();
            IQueryable testListOne = User.GenerateSampleModels(1).AsQueryable();
            IQueryable testListNone = User.GenerateSampleModels(0).AsQueryable();

            //Act
            var resultFull = testListFull.Any();
            var resultOne = testListOne.Any();
            var resultNone = testListNone.Any();

            //Assert
            Assert.True(resultFull);
            Assert.True(resultOne);
            Assert.False(resultNone);
        }

        [Fact]
        public void Any_Predicate()
        {
            //Arrange
            var queryable = User.GenerateSampleModels(100).AsQueryable();

            //Act
            bool expected = queryable.Any(u => u.Income > 50);
            bool result = (queryable as IQueryable).Any("Income > 50");

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Any_Dynamic_Select()
        {
            // Arrange
            IQueryable<User> queryable = User.GenerateSampleModels(1).AsQueryable();

            // Act
            var expected = queryable.Select(x => x.Roles.Any()).ToArray();
            var result = queryable.Select("Roles.Any()").ToDynamicArray<bool>();
            
            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Any_Dynamic_Where()
        {
            const string search = "e";

            // Arrange
            var testList = User.GenerateSampleModels(10);
            var queryable = testList.AsQueryable();

            // Act
            var expected = queryable.Where(u => u.Roles.Any(r => r.Name.Contains(search))).ToArray();
            var result = queryable.Where("Roles.Any(Name.Contains(@0))", search).ToArray();

            Assert.Equal(expected, result);
        }

        // https://dynamiclinq.codeplex.com/discussions/654313
        [Fact]
        public void Any_Dynamic_Where_Nested()
        {
            const string search = "a";

            // Arrange
            var testList = User.GenerateSampleModels(10);
            var queryable = testList.AsQueryable();

            // Act
            var expected = queryable.Where(u => u.Roles.Any(r => r.Permissions.Any(p => p.Name.Contains(search)))).ToArray();
            var result = queryable.Where("Roles.Any(Permissions.Any(Name.Contains(@0)))", search).ToArray();

            Assert.Equal(expected, result);
        }
    }
}