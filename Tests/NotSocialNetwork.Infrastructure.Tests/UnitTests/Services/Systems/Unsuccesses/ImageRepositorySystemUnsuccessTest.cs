using Moq;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Services.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Infrastructure.Tests.UnitTests.Services.Systems.Unsuccesses
{
    public class ImageRepositorySystemUnsuccessTest
    {
        private static ImageEntity _image = new ImageEntity()
        {
            Title = "Some title.jpg",
        };

        private static UpdateFileDTO _updateImage = new UpdateFileDTO()
        {
            FilePath = "SomePath",
            NewFile = null,
            OldFile = _image,
        };

        private static ImageEntity _defaultImage = new ImageEntity() 
        { 
            Id = DefaultImageConfig.DEFAULT_IMAGE_ID 
        };

        private static IEnumerable<ImageEntity> _images = new List<ImageEntity>()
        {
            new ImageEntity(),
            new ImageEntity()
        };


        [Fact]
        public async Task GetAsync_GetImageByIdIfNotFound_ObjectNotFoundException()
        {
            // Arrange
            var imageRepository = new Mock<IRepositoryAsync<ImageEntity>>();

            var imageRepositorySystem = new ImageRepositorySystem(
                                                imageRepository.Object);
            // Act
            Func<Task> act = async () => await imageRepositorySystem.GetAsync(_image.Id);

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(act);
        }

        [Fact]
        public async Task TrySaveAsync_ReturnDefaultImageIfImageIsNull_DefaultImage()
        {
            // Arrange
            var imageRepository = new Mock<IRepositoryAsync<ImageEntity>>();

            var imageRepositorySystem = new ImageRepositorySystem(
                                                imageRepository.Object);

            imageRepository.Setup(ir => ir.GetAsync(_defaultImage.Id))
                               .ReturnsAsync(_defaultImage);

            // Act
            var result = await imageRepositorySystem.TrySaveAsync(null);

            // Assert
            Assert.Equal(DefaultImageConfig.DEFAULT_IMAGE_ID, result.Id);
        }

        [Fact]
        public async Task TrySaveAsync_AddImageIfImageAlreadyExist_ObjectAlreadyExistException()
        {
            // Arrange
            var imageRepository = new Mock<IRepositoryAsync<ImageEntity>>();

            var imageRepositorySystem = new ImageRepositorySystem(
                                                imageRepository.Object);

            imageRepository.Setup(ir => ir.GetAll())
                .Returns(_images.AsQueryable());

            // Act
            Func<Task> act = async () => await imageRepositorySystem.TrySaveAsync(_images.ElementAt(0));

            // Assert
            await Assert.ThrowsAsync<ObjectAlreadyExistException>(act);
        }

        [Fact]
        public async Task TryUpdateAsync_UpdateIfNewFileIsNull_OldFileId()
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
            Assert.Equal(_updateImage.OldFile.Id, result);
        }
    }
}
