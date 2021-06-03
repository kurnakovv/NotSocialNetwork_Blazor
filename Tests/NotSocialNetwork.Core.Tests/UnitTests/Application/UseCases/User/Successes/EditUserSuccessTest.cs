using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.User;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.User.Successes
{
    public class EditUserSuccessTest
    {
        private readonly UserEntity _user = new UserEntity()
        {
            Name = "Name",
            DateOfBirth = DateTime.Now,
            Email = "email@gmail.com",
            Image = new ImageEntity() { Title = "Some title" },
        };

        [Fact]
        public async Task UpdateAsync_UpdateUser_User()
        {
            // Arrange
            var getableUser = new Mock<IGetableUser>();
            var userRepository = new Mock<IRepositoryAsync<UserEntity>>();

            var editUser = new EditUser(
                                    getableUser.Object,
                                    userRepository.Object);

            getableUser.Setup(gu => gu.GetById(_user.Id))
                                          .Returns(_user);

            _user.Name = "TestName";

            // Act
            var result = await editUser.UpdateAsync(_user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, result);
            Assert.Equal(_user.Id, result.Id);
            Assert.Equal(_user.Name, result.Name);
        }

        [Fact]
        public async Task DeleteAsync_DeleteUser_User()
        {
            // Arrange
            var getableUser = new Mock<IGetableUser>();
            var userRepository = new Mock<IRepositoryAsync<UserEntity>>();

            var userService = new EditUser(
                                    getableUser.Object,
                                    userRepository.Object);

            getableUser.Setup(gu => gu.GetById(_user.Id))
                                          .Returns(_user);

            // Act
            var result = await userService.DeleteAsync(_user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, result);
            Assert.Equal(_user.Id, result.Id);
        }
    }
}
