using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.User;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.User.Successes
{
    public class AddUserSuccessTest
    {
        private readonly UserEntity _user = new UserEntity()
        {
            Name = "Name",
            DateOfBirth = DateTime.Now,
            Email = "email@gmail.com",
            Image = new ImageEntity() { Title = "Some title" },
        };

        [Fact]
        public async Task AddAsync_AddUser_User()
        {
            // Arrange
            var getableUser = new Mock<IGetableUser>();
            var userRepository = new Mock<IRepositoryAsync<UserEntity>>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();

            var addUser = new AddUser(
                                    getableUser.Object,
                                    userRepository.Object,
                                    imageRepositorySystem.Object);

            // Act
            var result = await addUser.AddAsync(_user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, result);
            Assert.Equal(_user.Id, result.Id);
            Assert.Equal("Name", result.Name);
        }
    }
}
