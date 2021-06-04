using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Authentication.Login;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Authentication.Login.Successes
{
    public class AuthenticationControllerSuccessTest
    {
        private readonly LoginDTO _loginDTO = new LoginDTO()
        {
            Email = "login@gmail.com"
        };

        private readonly UserEntity _user = new UserEntity() { };

        [Fact]
        public void Login_LoginIfFound_OkObjectResult()
        {
            // Arrange
            var jwtSystem = new Mock<IJwtSystem>();
            var getUser = new Mock<IGetableUser>();

            var authController = new AuthenticationController(
                                        jwtSystem.Object,
                                        getUser.Object);

            getUser.Setup(gu => gu.GetByEmail(_loginDTO.Email)).Returns(_user);
            jwtSystem.Setup(js => js.GenerateToken(_user));


            // Act
            var result = authController.Login(_loginDTO);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
