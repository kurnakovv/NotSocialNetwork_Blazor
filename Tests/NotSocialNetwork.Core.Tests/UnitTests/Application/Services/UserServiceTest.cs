using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.Services
{

    public class UserServiceTest
    {
        private readonly IEnumerable<UserEntity> _users = new List<UserEntity>()
        {
            new UserEntity()
            {
                Name = "Name1",
                DateOfBirth = DateTime.Now,
                Email = "firstEmail@gmail.com",
            },
            new UserEntity()
            {
                Name = "Name2",
                DateOfBirth = DateTime.Now,
                Email = "lastEmail@gmail.com",
            },
        };

        private readonly UserEntity _user = new()
        {
            Name = "Name",
            DateOfBirth = DateTime.Now,
            Email = "email@gmail.com",
        };

        [Fact]
        public void GetAll_GetAllUsers_Users()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(userRepositoryMock.Object);

            userRepositoryMock.Setup(r => r.GetAll())
                                               .Returns(_users.AsQueryable());

            // Act
            var result = userService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users, result);
            Assert.Equal("Name1", result.ElementAt(0).Name);
            Assert.Equal("Name2", result.ElementAt(1).Name);
        }

        [Fact]
        public void Add_AddUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(userRepositoryMock.Object);

            // Act
            var result = userService.Add(_user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, result);
            Assert.Equal(_user.Id, result.Id);
            Assert.Equal("Name", result.Name);
        }

        [Fact]
        public void Add_AddInvalidUser_ObjectAlreadyExistException()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(userRepositoryMock.Object);

            userRepositoryMock.Setup(r => r.Add(_user))
                                .Throws(new ObjectAlreadyExistException($"User by Id: {_user.Id} already exists."));

            // Act
            Action act = () => userService.Add(_user);

            // Assert
            Assert.Throws<ObjectAlreadyExistException>(act);
        }

        [Fact]
        public void Delete_DeleteUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(userRepositoryMock.Object);

            userRepositoryMock.Setup(r => r.Get(_user.Id))
                                               .Returns(_user);

            // Act
            var result = userService.Delete(_user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, result);
            Assert.Equal(_user.Id, result.Id);
            Assert.DoesNotContain(_user, userService.GetAll());
        }

        [Fact]
        public void Delete_DeleteInvalidUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(userRepositoryMock.Object);

            // Act
            Action act = () => userService.Delete(_user.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void Get_GetUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(userRepositoryMock.Object);

            userRepositoryMock.Setup(r => r.Get(_user.Id))
                                               .Returns(_user);

            // Act
            var result = userService.GetById(_user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, result);
            Assert.Equal(_user.Id, result.Id);
            Assert.Equal(_user.Name, result.Name);
        }

        [Fact]
        public void Get_GetInvalidUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(userRepositoryMock.Object);

            // Act
            Action act = () => userService.GetById(_user.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void Update_UpdateUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(userRepositoryMock.Object);

            userRepositoryMock.Setup(r => r.Get(_user.Id))
                                               .Returns(_user);
            _user.Name = "TestName";

            // Act
            var result = userService.Update(_user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, result);
            Assert.Equal(_user.Id, result.Id);
            Assert.Equal(_user.Name, result.Name);
        }

        [Fact]
        public void Update_UpdateInvalidUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(userRepositoryMock.Object);
            _user.Name = "TestName";

            // Act
            Action act = () => userService.Update(_user);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }
    }
}
