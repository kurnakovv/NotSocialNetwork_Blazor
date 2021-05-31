using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.User;
using System;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.User.Unsuccesses
{
    public class AddUserUnsuccessTest
    {
        private readonly UserEntity _user = new UserEntity()
        {
            Name = "Name",
            DateOfBirth = DateTime.Now,
            Email = "email@gmail.com",
            Image = new ImageEntity() { Title = "Some title" },
        };

        [Fact]
        public void Add_AddInvalidUser_ObjectAlreadyExistException()
        {
            // Arrange
            var getableUser = new Mock<IGetableUser>();
            var userRepository = new Mock<IRepository<UserEntity>>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();

            var addUser = new AddUser(
                                    getableUser.Object,
                                    userRepository.Object,
                                    imageRepositorySystem.Object);

            userRepository.Setup(r => r.Add(_user))
                                .Throws(new ObjectAlreadyExistException($"User by Id: {_user.Id} already exists."));

            // Act
            Action act = () => addUser.Add(_user);

            // Assert
            Assert.Throws<ObjectAlreadyExistException>(act);
        }
    }
}
