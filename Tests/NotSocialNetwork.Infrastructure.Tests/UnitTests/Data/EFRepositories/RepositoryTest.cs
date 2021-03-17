using Moq;
using NotSocialNetwork.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace NotSocialNetwork.Infrastructure.Tests.Data.EFRepositories
{
    public class RepositoryTest
    {
        private readonly IEnumerable<MockObject> _objects = new List<MockObject>()
        {
            new MockObject()
            {
                Id = Guid.NewGuid(),
                Name = "Name1",
            },
            new MockObject()
            {
                Id = Guid.NewGuid(),
                Name = "Name2",
            },
        };

        private readonly MockObject _object = new()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
        };

        [Fact]
        public void GetAll_GetAllObjects_Objects()
        {
            // Arrange
            var repository = new Mock<IRepository<MockObject>>();

            // Act
            var result = repository.Setup(r => r.GetAll()).Returns((IQueryable<MockObject>)_objects);

            // Assert
            Assert.Equal(_objects, repository.Object.GetAll());
        }

        [Fact]
        public void Add_AddObject_Object()
        {
            // Arrange
            var repository = new Mock<IRepository<MockObject>>();

            // Act
            var result = repository.Setup(u => u.Add(_object)).Returns(_object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_object, repository.Object.Add(_object));
        }

        [Fact]
        public void Delete_DeleteObject_Object()
        {
            // Arrange
            var repository = new Mock<IRepository<MockObject>>();

            // Act
            var result = repository.Setup(u => u.Delete(_object.Id)).Returns(_object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_object, repository.Object.Delete(_object.Id));
        }

        [Fact]
        public void Get_GetObject_Object()
        {
            // Arrange
            var repository = new Mock<IRepository<MockObject>>();

            // Act
            var result = repository.Setup(u => u.Get(_object.Id)).Returns(_object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_object, repository.Object.Get(_object.Id));
        }

        [Fact]
        public void Update_UpdateObject_Object()
        {
            // Arrange
            var repository = new Mock<IRepository<MockObject>>();

            // Act
            var result = repository.Setup(u => u.Update(_object)).Returns(_object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_object, repository.Object.Update(_object));
        }
    }

    public class MockObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
