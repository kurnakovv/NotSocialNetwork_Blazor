using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Admin.User.Get;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using System.Collections.Generic;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Admin.User.Get.Successes
{
    public class AdminUserControllerSuccessTest
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

            var adminUserController = new AdminUserController(
                                            getableUser.Object,
                                            mapper.Object);

            // Act
            var results = adminUserController.Get();

            // Assert
            Assert.NotNull(adminUserController);
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

            var adminUserController = new AdminUserController(
                                            getableUser.Object,
                                            mapper.Object);

            // Act
            var result = adminUserController.Get(_user.Id);

            // Assert
            Assert.NotNull(adminUserController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
