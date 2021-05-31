using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.UseCases.User;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.User.Successes
{
    public class GetUserSuccessTest
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

        [Fact]
        public void GetAll_GetAllUsers_Users()
        {
            // Arrange
            var userRepository = new Mock<IRepository<UserEntity>>();
            var userService = new GetUser(
                                    userRepository.Object);

            userRepository.Setup(r => r.GetAll())
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
        public void GetById_GetUser_User()
        {
            // Arrange
            var userRepository = new Mock<IRepository<UserEntity>>();

            var userService = new GetUser(
                                    userRepository.Object);

            userRepository.Setup(r => r.GetAll())
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
        public void GetByEmail_GetUser_User()
        {
            // Arrange
            var userRepository = new Mock<IRepository<UserEntity>>();

            var userService = new GetUser(
                                    userRepository.Object);

            userRepository.Setup(r => r.GetAll())
                              .Returns(_users.AsQueryable());

            // Act
            var result = userService.GetByEmail(_users.ElementAt(0).Email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users.ElementAt(0).Email, result.Email);
        }

        [Fact]
        public void GetByPagination_GetUsers_Users()
        {
            // Arrange
            var userRepository = new Mock<IRepository<UserEntity>>();

            var userService = new GetUser(
                                    userRepository.Object);

            userRepository.Setup(r => r.GetAll())
                              .Returns(_users.AsQueryable());

            // Act
            var result = userService.GetByPagination(0);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users, result);
        }
    }
}
