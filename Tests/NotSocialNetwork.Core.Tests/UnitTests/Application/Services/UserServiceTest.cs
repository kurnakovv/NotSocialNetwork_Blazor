using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
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
                Image = new ImageEntity() { Title = "Some title1"},
            },
            new UserEntity()
            {
                Name = "Name2",
                DateOfBirth = DateTime.Now,
                Email = "lastEmail@gmail.com",
                Image = new ImageEntity() { Title = "Some title2"},
            },
        };

        private readonly UserEntity _user = new()
        {
            Name = "Name",
            DateOfBirth = DateTime.Now,
            Email = "email@gmail.com",
            Image = new ImageEntity() { Title = "Some title"},
        };

        [Fact]
        public void GetAll_GetAllUsers_Users()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);

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
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    imageRepositorySystem.Object);

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
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    imageRepositorySystem.Object);

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
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);

            userRepositoryMock.Setup(r => r.GetAll())
                                               .Returns(_users.AsQueryable());

            // Act
            var result = userService.Delete(_users.ElementAt(0).Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users.ElementAt(0), result);
            Assert.Equal(_users.ElementAt(0).Id, result.Id);
        }

        [Fact]
        public void Delete_DeleteInvalidUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);

            // Act
            Action act = () => userService.Delete(_user.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void GetById_GetUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);

            userRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(_users.AsQueryable());

            // Act
            var result = userService.GetById(_users.ElementAt(0).Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users.ElementAt(0), result);
            Assert.Equal(_users.ElementAt(0).Id, result.Id);
            Assert.Equal(_users.ElementAt(0).Name, result.Name);
        }

        [Fact]
        public void GetById_GetInvalidUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);

            // Act
            Action act = () => userService.GetById(_user.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void GetByEmail_GetUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);

            userRepositoryMock.Setup(r => r.GetAll())
                                               .Returns(_users.AsQueryable());

            // Act
            var result = userService.GetByEmail(_users.ElementAt(0).Email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users.ElementAt(0).Email, result.Email);
        }

        [Fact]
        public void GetByEmail_GetInvalidUser_ObjectNotFoundException()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);

            // Act
            Action act = () => userService.GetByEmail(_user.Email);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void GetByPagination_GetUsers_Users()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);

            userRepositoryMock.Setup(r => r.GetAll())
                                               .Returns(_users.AsQueryable());

            // Act
            var result = userService.GetByPagination(0);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users, result);
        }

        [Fact]
        public void GetByPagination_GetUsersIfIndexLessZero_InvalidOperationException()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);
            int invalidIndex = -1;

            // Act
            Action act = () => userService.GetByPagination(invalidIndex);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void GetByPagination_GetUsersIfUsersEnded_ObjectNotFoundException()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);
            int bigIndex = 10;

            userRepositoryMock.Setup(r => r.GetAll())
                                               .Returns(_users.AsQueryable());

            // Act
            Action act = () => userService.GetByPagination(bigIndex);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void Update_UpdateUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);

            userRepositoryMock.Setup(r => r.GetAll())
                                               .Returns(_users.AsQueryable());
            _users.ElementAt(0).Name = "TestName";

            // Act
            var result = userService.Update(_users.ElementAt(0));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users.ElementAt(0), result);
            Assert.Equal(_users.ElementAt(0).Id, result.Id);
            Assert.Equal(_users.ElementAt(0).Name, result.Name);
        }

        [Fact]
        public void Update_UpdateInvalidUser_User()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<UserEntity>>();
            var userService = new UserService(
                                    userRepositoryMock.Object,
                                    null);
            _user.Name = "TestName";

            // Act
            Action act = () => userService.Update(_user);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }
    }
}
