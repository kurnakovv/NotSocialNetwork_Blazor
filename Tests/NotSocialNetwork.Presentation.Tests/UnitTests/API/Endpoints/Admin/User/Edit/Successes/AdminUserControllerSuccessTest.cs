using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Admin.User.Edit;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Admin.User.Edit.Successes
{
    public class AdminUserControllerSuccessTest
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
        public async Task Update_UpdateUser_OkObjectResult()
        {
            // Arrange
            var editableUser = new Mock<IEditableUserAsync>();
            var mapper = new Mock<IMapper>();

            editableUser.Setup(eu => eu.UpdateAsync(_user).Result)
                .Returns(_user);

            var adminUserController = new AdminUserController(
                                        editableUser.Object,
                                        mapper.Object);

            // Act
            var result = await adminUserController.Update(_user);

            // Assert
            Assert.NotNull(adminUserController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Delete_DeleteUserById_OkObjectResult()
        {
            // Arrange
            var editableUser = new Mock<IEditableUserAsync>();
            var mapper = new Mock<IMapper>();

            editableUser.Setup(eu => eu.DeleteAsync(_user.Id).Result)
                .Returns(_user);

            var adminUserController = new AdminUserController(
                                        editableUser.Object,
                                        mapper.Object);

            // Act
            var result = await adminUserController.Delete(_user.Id);

            // Assert
            Assert.NotNull(adminUserController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
