using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.User.Register;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.User.Register.Unsuccesses
{
    public class UserControllerUnsuccessTest
    {
        private static readonly UserEntity _user = new UserEntity()
        {
            Name = "Maksim",
            DateOfBirth = DateTime.Now,
            Email = "maksim@gmail.com",
            Image = new ImageEntity()
            {
                Title = "Title.ggfdsdfsgfsdff",
            },
        };
        private readonly RegistrationUserDTO _userDTO = new()
        {
            Name = "Maksim",
            DateOfBirth = DateTime.Now,
            Email = "maksim@gmail.com",
        };
        private static readonly RegistrationResponseDTO _registrationResponseDTO = new()
        {
            Id = _user.Id,
        };

        [Fact]
        public async Task Register_RegisterUserIfImageFormatInvalid_BadRequest400()
        {
            // Arrange
            var addUser = new Mock<IAddableUserAsync>();
            var mapper = new Mock<IMapper>();
            var jwtSystem = new Mock<IJwtSystem>();

            var userController = new UserController(
                                        addUser.Object,
                                        mapper.Object,
                                        jwtSystem.Object);

            addUser.Setup(au => au.AddAsync(_user).Result)
                .Throws(new InvalidFileFormatException("Invalid image format."));
            mapper.Setup(m => m.Map<UserEntity>(_userDTO)).Returns(_user);
            mapper.Setup(m => m.Map<RegistrationResponseDTO>(_user)).Returns(_registrationResponseDTO);

            // Act
            var result = await userController.Register(_userDTO);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Register_RegisterUserIfUserAlreadyExist_BadRequest400()
        {
            // Arrange
            var addUser = new Mock<IAddableUserAsync>();
            var mapper = new Mock<IMapper>();
            var jwtSystem = new Mock<IJwtSystem>();

            var userController = new UserController(
                                        addUser.Object,
                                        mapper.Object,
                                        jwtSystem.Object);

            addUser.Setup(au => au.AddAsync(_user).Result)
                .Throws(new ObjectAlreadyExistException("User already exist."));
            mapper.Setup(m => m.Map<UserEntity>(_userDTO)).Returns(_user);
            mapper.Setup(m => m.Map<RegistrationResponseDTO>(_user)).Returns(_registrationResponseDTO);

            // Act
            var result = await userController.Register(_userDTO);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
