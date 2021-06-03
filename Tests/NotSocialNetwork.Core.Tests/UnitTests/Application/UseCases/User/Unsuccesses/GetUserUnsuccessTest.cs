using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.UseCases.User;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.User.Unsuccesses
{
    public class GetUserUnsuccessTest
    {
        private readonly List<UserEntity> _users = new List<UserEntity>()
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

        private readonly UserEntity _user = new UserEntity()
        {
            Name = "Name",
            DateOfBirth = DateTime.Now,
            Email = "email@gmail.com",
            Image = new ImageEntity() { Title = "Some title" },
        };

        [Fact]
        public void GetById_GetInvalidUser_ObjectNotFoundException()
        {
            // Arrange
            var userRepository = new Mock<IRepositoryAsync<UserEntity>>();
            var getUser = new GetUser(
                                    userRepository.Object);

            // Act
            Action act = () => getUser.GetById(_user.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void GetByEmail_GetInvalidUser_ObjectNotFoundException()
        {
            // Arrange
            var userRepository = new Mock<IRepositoryAsync<UserEntity>>();

            var getUser = new GetUser(
                                    userRepository.Object);

            // Act
            Action act = () => getUser.GetByEmail(_user.Email);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void GetByPagination_GetUsersIfIndexLessZero_InvalidOperationException()
        {
            // Arrange
            var userRepository = new Mock<IRepositoryAsync<UserEntity>>();

            var getUser = new GetUser(
                                    userRepository.Object);

            int invalidIndex = -1;

            // Act
            Action act = () => getUser.GetByPagination(invalidIndex);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void GetByPagination_GetUsersIfUsersEnded_ObjectNotFoundException()
        {
            // Arrange
            var userRepository = new Mock<IRepositoryAsync<UserEntity>>();

            var getUser = new GetUser(
                                    userRepository.Object);

            int bigIndex = 10;

            userRepository.Setup(r => r.GetAll())
                              .Returns(_users.AsQueryable());

            // Act
            Action act = () => getUser.GetByPagination(bigIndex);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }
    }
}
