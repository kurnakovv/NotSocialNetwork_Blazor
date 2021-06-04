using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Services.Systems;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Infrastructure.Tests.UnitTests.Services.Systems.Unsuccesses
{
    public class ImageFileSystemUnsuccessTest
    {
        private static ImageEntity _image = new ImageEntity()
        {
            Title = "image.jpg"
        };

        [Fact]
        public async Task DeleteAsync_DeleteIfFileNotFound_ObjectNotFoundException()
        {
            // Arrange
            var imageRepository = new Mock<IRepositoryAsync<ImageEntity>>();

            var imageFileSystem = new ImageFileSystem(
                                          imageRepository.Object);

            imageRepository.Setup(ir => ir.GetAsync(_image.Id))
                               .ReturnsAsync(_image);

            // Act
            Func<Task> act = async () => await imageFileSystem.DeleteAsync(_image.Id, "SomePath");

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(act);
        }
    }
}
