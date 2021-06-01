using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.User.Get;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using System.Collections.Generic;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.User.Get.Successes
{
    public class UserControllerSuccessTest
    {
        private readonly IEnumerable<UserEntity> _users = new List<UserEntity>()
        {
            new UserEntity(){Name = "Maksim", DateOfBirth = DateTime.Now, Email = "maksim@gmail.com" },
            new UserEntity(){Name = "Ivan", DateOfBirth = DateTime.Now, Email = "ivan@gmail.com" },
        };

        private static readonly UserEntity _user = new UserEntity()
        {
            Name = "Maksim",
            DateOfBirth = DateTime.Now,
            Email = "maksim@gmail.com",
            Image = new ImageEntity()
            {
                Title = "Title.jpg",
            },
        };

        [Fact]
        public void Get_GetAllUsers_OkObjectResult()
        {
            // Arrange
            var getableUser = new Mock<IGetableUser>();
            var mapper = new Mock<IMapper>();

            getableUser.Setup(gu => gu.GetAll())
                .Returns(_users);

            var userController = new UserController(
                                        getableUser.Object,
                                        mapper.Object);

            // Act
            var results = userController.Get();

            // Assert
            Assert.NotNull(userController);
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results.Result);
        }

        [Fact]
        public void Get_GetUserById_OkObjectResult()
        {
            // Arrange
            var getableUser = new Mock<IGetableUser>();
            var mapper = new Mock<IMapper>();

            getableUser.Setup(gu => gu.GetById(_user.Id))
                .Returns(_user);

            var userController = new UserController(
                                        getableUser.Object,
                                        mapper.Object);

            // Act
            var result = userController.Get(_user.Id);

            // Assert
            Assert.NotNull(userController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
