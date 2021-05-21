using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Moq;
using NotSocialNetwork.API.Controllers;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using Xunit;
using NotSocialNetwork.Application.Interfaces.Facades;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Controllers
{
    public class UserControllerTest
    {
        private readonly IEnumerable<UserEntity> _users = new List<UserEntity>()
        {
            new UserEntity(){Name = "Maksim", DateOfBirth = DateTime.Now, Email = "maksim@gmail.com" },
            new UserEntity(){Name = "Ivan", DateOfBirth = DateTime.Now, Email = "ivan@gmail.com" },
        };

        private readonly UserEntity _user = new UserEntity()
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

        [Fact]
        public void Get_GetAllUsers_OkObjectResult()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var mapper = new Mock<IMapper>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();

            userService.Setup(u => u.GetAll())
                .Returns(_users);
            var userController = new UserController(
                                        userService.Object,
                                        mapper.Object,
                                        imageRepositorySystem.Object);

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
            var userService = new Mock<IUserService>();
            var mapper = new Mock<IMapper>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();

            userService.Setup(u => u.GetById(_user.Id))
                .Returns(_user);
            var userController = new UserController(
                                        userService.Object,
                                        mapper.Object,
                                        imageRepositorySystem.Object);

            // Act
            var result = userController.Get(_user.Id);

            // Assert
            Assert.NotNull(userController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Add_AddUser_OkObjectResult()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var mapper = new Mock<IMapper>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();


            userService.Setup(u => u.Add(_user))
                .Returns(_user);

            //hostEnvironment.Setup(h => h.EnvironmentName)
            //    .Returns("Hosting:UnitTestEnvironment");

            //imageFacade.Setup(i => i.Save(_user.Image, "pathToSave"))
            //    .Returns(_user.Id);

            var userController = new UserController(
                                        userService.Object,
                                        mapper.Object,
                                        imageRepositorySystem.Object);

            // Act
            var result = userController.Add(_userDTO);

            // Assert
            Assert.NotNull(userController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Update_UpdateUser_OkObjectResult()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var mapper = new Mock<IMapper>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();

            userService.Setup(u => u.Update(_user))
                .Returns(_user);

            var userController = new UserController(
                                        userService.Object,
                                        mapper.Object,
                                        imageRepositorySystem.Object);

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
            var userService = new Mock<IUserService>();
            var mapper = new Mock<IMapper>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();

            userService.Setup(u => u.Delete(_user.Id))
                .Returns(_user);

            var userController = new UserController(
                                        userService.Object,
                                        mapper.Object,
                                        imageRepositorySystem.Object);

            // Act
            var result = userController.Delete(_user.Id);

            // Assert
            Assert.NotNull(userController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
