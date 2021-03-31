using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
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
            var userService = new Mock<IUserService>();
            
            // Act
            var result = userService.Setup(u => u.GetAll()).Returns(_users);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users, userService.Object.GetAll());
        }

        [Fact]
        public void Add_AddUser_User()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            // Act
            var result = userService.Setup(u => u.Add(_user)).Returns(_user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, userService.Object.Add(_user));
        }

        [Fact]
        public void Delete_DeleteUser_User()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            // Act
            var result = userService.Setup(u => u.Delete(_user.Id)).Returns(_user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, userService.Object.Delete(_user.Id));
        }

        [Fact]
        public void Get_GetUser_User()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            // Act
            var result = userService.Setup(u => u.GetById(_user.Id)).Returns(_user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, userService.Object.GetById(_user.Id));
        }

        [Fact]
        public void Update_UpdateUser_User()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            // Act
            var result = userService.Setup(u => u.Update(_user)).Returns(_user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, userService.Object.Update(_user));
        }
    }
}
