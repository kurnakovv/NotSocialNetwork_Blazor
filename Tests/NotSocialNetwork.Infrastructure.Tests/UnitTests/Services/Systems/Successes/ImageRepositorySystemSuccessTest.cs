using Moq;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Services.Systems;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Infrastructure.Tests.UnitTests.Services.Systems.Successes
{
    public class ImageRepositorySystemSuccessTest
    {
        private static ImageEntity _image = new ImageEntity()
        {
            Title = "Some title.jpg",
        };

        private static UpdateFileDTO _updateImage = new UpdateFileDTO()
        {
            FilePath = "SomePath",
            NewFile = new ImageEntity() { Title = "Title" },
            OldFile = _image,
        };

        [Fact]
        public async Task GetAsync_GetImageById_Image()
        {
            // Arrange
            var imageRepository = new Mock<IRepositoryAsync<ImageEntity>>();

            var imageRepositorySystem = new ImageRepositorySystem(
                                                imageRepository.Object);

            imageRepository.Setup(ir => ir.GetAsync(_image.Id))
                               .ReturnsAsync(_image);

            // Act
            var result = await imageRepositorySystem.GetAsync(_image.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_image, result);
        }

        [Fact]
        public async Task TrySaveAsync_SaveImage_Image()
        {
            // Arrange
            var imageRepository = new Mock<IRepositoryAsync<ImageEntity>>();

            var imageRepositorySystem = new ImageRepositorySystem(
                                                imageRepository.Object);

            // Act
            var result = await imageRepositorySystem.TrySaveAsync(_image);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_image, result);
        }

        [Fact]
        public async Task TryUpdateAsync_UpdateImage_NewImage()
        {
            // Arrange
            var imageRepository = new Mock<IRepositoryAsync<ImageEntity>>();

            var imageRepositorySystem = new ImageRepositorySystem(
                                                imageRepository.Object);

            imageRepository.Setup(ir => ir.DeleteAsync(_updateImage.OldFile.Id))
                               .ReturnsAsync(_image);
            imageRepository.Setup(ir => ir.AddAsync(_updateImage.NewFile as ImageEntity))
                               .ReturnsAsync(_image);

            // Act
            var result = await imageRepositorySystem.TryUpdateAsync(_updateImage);

            // Assert
            Assert.Equal(_updateImage.NewFile.Id, result);
        }

        [Fact]
        public async Task TryDeleteAsync_DeleteImageById_Id()
        {
            // Arrange
            var imageRepository = new Mock<IRepositoryAsync<ImageEntity>>();

            var imageRepositorySystem = new ImageRepositorySystem(
                                                imageRepository.Object);

            // Act
            var result = await imageRepositorySystem.TryDeleteAsync(_image.Id);

            // Assert
            Assert.Equal(_image.Id, result);
        }
    }
}
