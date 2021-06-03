using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.User.Unsuccesses
{
    public class AddUserUnsuccessTest
    {
        private readonly IEnumerable<UserEntity> _users = new List<UserEntity>()
        {
            new UserEntity()
            {
                Name = "Name1",
                DateOfBirth = DateTime.Now,
                Email = "firstEmail@gmail.com",
                Image = new ImageEntity() { Title = "Some title1"},
            },
            new UserEntity()
            {
                Name = "Name2",
                DateOfBirth = DateTime.Now,
                Email = "lastEmail@gmail.com",
                Image = new ImageEntity() { Title = "Some title2"},
            },
        };

        [Fact]
        public async Task AddAsync_AddInvalidUser_ObjectAlreadyExistException()
        {
            // Arrange
            var getableUser = new Mock<IGetableUser>();
            var userRepository = new Mock<IRepositoryAsync<UserEntity>>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();

            var addUser = new AddUser(
                                    getableUser.Object,
                                    userRepository.Object,
                                    imageRepositorySystem.Object);

            getableUser.Setup(gu => gu.GetAll())
                           .Returns(_users);

            // Act
            Func<Task> act = async () => await addUser.AddAsync(_users.ElementAt(0));

            // Assert
            await Assert.ThrowsAsync<ObjectAlreadyExistException>(act);
        }
    }
}
