using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.User;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.User.Unsuccesses
{
    public class EditUserUnsuccessTest
    {
        private readonly UserEntity _user = new UserEntity()
        {
            Name = "Name",
            DateOfBirth = DateTime.Now,
            Email = "email@gmail.com",
            Image = new ImageEntity() { Title = "Some title" },
        };

        [Fact]
        public async Task DeleteAsync_DeleteInvalidUser_ObjectNotFoundException()
        {
            // Arrange
            var getableUser = new Mock<IGetableUser>();
            var userRepository = new Mock<IRepositoryAsync<UserEntity>>();

            var editUser = new EditUser(
                                    getableUser.Object,
                                    userRepository.Object);

            getableUser.Setup(gu => gu.GetById(_user.Id))
                           .Throws(new ObjectNotFoundException($"User by Id:{_user.Id} not found."));

            // Act
            Func<Task> act = async () => await editUser.DeleteAsync(_user.Id);

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(act);
        }

        [Fact]
        public async Task UpdateAsync_UpdateInvalidUser_ObjectNotFoundException()
        {
            // Arrange
            var getableUser = new Mock<IGetableUser>();
            var userRepository = new Mock<IRepositoryAsync<UserEntity>>();

            var editUser = new EditUser(
                                    getableUser.Object,
                                    userRepository.Object);

            getableUser.Setup(gu => gu.GetById(_user.Id))
                           .Throws(new ObjectNotFoundException($"User by Id:{_user.Id} not found."));

            _user.Name = "TestName";

            // Act
            Func<Task> act = async () => await editUser.UpdateAsync(_user);

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(act);
        }
    }
}
