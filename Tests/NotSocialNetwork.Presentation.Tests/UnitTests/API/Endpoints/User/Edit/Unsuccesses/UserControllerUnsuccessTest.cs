using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.User.Edit;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.User.Edit.Unsuccesses
{
    public class UserControllerUnsuccessTest
    {
        private UserEntity _user = new UserEntity();

        [Fact]
        public async Task Update_UpdateUser_NotFound404()
        {
            // Arrange
            var editUser = new Mock<IEditableUserAsync>();
            var mapper = new Mock<IMapper>();

            var userController = new UserController(
                                        editUser.Object,
                                        mapper.Object);

            editUser.Setup(eu => eu.UpdateAsync(_user))
                .Throws(new ObjectNotFoundException("Some message."));

            // Act
            var result = await userController.Update(_user);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Delete_DeleteUser_NotFound404()
        {
            // Arrange
            var editUser = new Mock<IEditableUserAsync>();
            var mapper = new Mock<IMapper>();

            var userController = new UserController(
                                        editUser.Object,
                                        mapper.Object);

            editUser.Setup(eu => eu.DeleteAsync(_user.Id))
                .Throws(new ObjectNotFoundException("Some message."));

            // Act
            var result = await userController.Delete(_user.Id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
