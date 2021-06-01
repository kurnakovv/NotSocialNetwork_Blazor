using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.User.Register;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using System.Collections.Generic;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.User.Register.Successes
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
        public void Add_AddUser_OkObjectResult()
        {
            // Arrange
            var addableUser = new Mock<IAddableUser>();
            var mapper = new Mock<IMapper>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();
            var jwtSystem = new Mock<IJwtSystem>();


            addableUser.Setup(au => au.Add(_user))
                .Returns(_user);
            mapper.Setup(m => m.Map<UserEntity>(_userDTO)).Returns(_user);
            mapper.Setup(m => m.Map<RegistrationResponseDTO>(_user)).Returns(_registrationResponseDTO);

            var userController = new UserController(
                                        addableUser.Object,
                                        mapper.Object,
                                        jwtSystem.Object);

            // Act
            var result = userController.Register(_userDTO);

            // Assert
            Assert.NotNull(userController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
