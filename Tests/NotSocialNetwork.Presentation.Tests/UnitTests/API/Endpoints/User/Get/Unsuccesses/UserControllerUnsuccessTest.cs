using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.User.Get;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.User.Get.Unsuccesses
{
    public class UserControllerUnsuccessTest
    {
        private UserEntity _user = new UserEntity();
        [Fact]
        public void Get_GetUsersIfUsersNoMore_NotFound404()
        {
            // Arrange
            var getUser = new Mock<IGetableUser>();
            var mapper = new Mock<IMapper>();

            var userController = new UserController(
                                    getUser.Object,
                                    mapper.Object);
            var invalidIndex = -1;

            getUser.Setup(gu => gu.GetByPagination(invalidIndex))
                       .Throws(new ObjectNotFoundException("Invalid index."));

            // Act
            var result = userController.Get(invalidIndex);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void Get_GetUsersIfIndexInvalid_BadRequest400()
        {
            // Arrange
            var getUser = new Mock<IGetableUser>();
            var mapper = new Mock<IMapper>();

            var userController = new UserController(
                                    getUser.Object,
                                    mapper.Object);
            var validIndex = 0;

            getUser.Setup(gu => gu.GetByPagination(validIndex))
                       .Throws(new ObjectNotFoundException("No more users."));

            // Act
            var result = userController.Get(validIndex);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void Get_GetUserByIdIfUserNotFound_NotFound404()
        {
            // Arrange
            var getUser = new Mock<IGetableUser>();
            var mapper = new Mock<IMapper>();

            var userController = new UserController(
                                    getUser.Object,
                                    mapper.Object);
            
            getUser.Setup(gu => gu.GetById(_user.Id))
                       .Throws(new ObjectNotFoundException("User not found."));

            // Act
            var result = userController.Get(_user.Id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
