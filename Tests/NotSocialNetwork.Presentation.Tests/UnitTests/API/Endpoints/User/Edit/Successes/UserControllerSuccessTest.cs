using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.User.Edit;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.User.Edit.Successes
{
    public class UserControllerSuccessTest
    {
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
        public void Update_UpdateUser_OkObjectResult()
        {
            // Arrange
            var editableUser = new Mock<IEditableUser>();
            var mapper = new Mock<IMapper>();

            editableUser.Setup(eu => eu.Update(_user))
                .Returns(_user);

            var userController = new UserController(
                                        editableUser.Object,
                                        mapper.Object);

            // Act
            var result = userController.Update(_user);

            // Assert
            Assert.NotNull(userController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Delete_DeleteUserById_OkObjectResult()
        {
            // Arrange
            var editableUser = new Mock<IEditableUser>();
            var mapper = new Mock<IMapper>();

            editableUser.Setup(eu => eu.Delete(_user.Id))
                .Returns(_user);

            var userController = new UserController(
                                        editableUser.Object,
                                        mapper.Object);

            // Act
            var result = userController.Delete(_user.Id);

            // Assert
            Assert.NotNull(userController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
