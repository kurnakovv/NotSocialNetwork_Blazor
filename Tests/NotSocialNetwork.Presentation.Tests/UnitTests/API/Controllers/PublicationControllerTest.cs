using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Controllers;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Managers;
using NotSocialNetwork.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.Extensions.Hosting;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Controllers
{
    public class PublicationControllerTest
    {
        private static UserEntity _author = new UserEntity()
        {
            Name = "Ivan",
            DateOfBirth = DateTime.Now,
            Email = "ivan@gmail.com"
        };
        private static IEnumerable<PublicationDTO> _publicationsDTO = new List<PublicationDTO>()
        {
            new PublicationDTO()
            { 
                Author = _author,
                Text = "Some text1",
            },
            new PublicationDTO()
            {
                Author = _author,
                Text = "Some text2",
            },
        };
        private static IEnumerable<PublicationEntity> _publicationEntities = new List<PublicationEntity>()
        {
            new PublicationEntity()
            {
                Author = _publicationsDTO.ElementAt(0).Author,
                Text = _publicationsDTO.ElementAt(0).Text,
            },
            new PublicationEntity()
            {
                Author = _publicationsDTO.ElementAt(1).Author,
                Text = _publicationsDTO.ElementAt(1).Text,
            },
        };
        private static AddPublicationDTO _appPublicationDTO = new AddPublicationDTO()
        { 
            Text = "Some text",
            AuthorId = _author.Id,
            Title = "Some title",
        };
        private static UpdatePublicationDTO _updatePublicationDTO = new UpdatePublicationDTO() 
        {
            Id = _publicationEntities.ElementAt(0).Id,
            Text = "Update text",
        };

        [Fact]
        public void Get_GetAllPublications_Publications()
        {
            // Arrange
            var publicationService = new Mock<IPublicationService>();
            var mapper = new Mock<IMapper>();

            publicationService.Setup(p => p.GetAll())
                .Returns(_publicationEntities);

            mapper.Setup(p => p.Map<IEnumerable<PublicationDTO>>(_publicationEntities))
                .Returns(_publicationsDTO);

            var publicationController = new PublicationController(
                                            publicationService.Object,
                                            mapper.Object,
                                            null,
                                            null);

            // Act
            var results = publicationController.Get();

            // Assert
            Assert.NotNull(publicationController);
            Assert.NotNull(results);
            Assert.Equal("Some text1", results.ElementAt(0).Text);
            Assert.Equal("Some text2", results.ElementAt(1).Text);
        }

        [Fact]
        public void Get_GetPublicationById_Publication()
        {
            // Arrange
            var publicationService = new Mock<IPublicationService>();
            var mapper = new Mock<IMapper>();

            publicationService.Setup(p => p.GetById(_publicationEntities.ElementAt(0).Id))
                .Returns(_publicationEntities.ElementAt(0));

            mapper.Setup(m => m.Map<PublicationDTO>(_publicationEntities.ElementAt(0)))
                .Returns(_publicationsDTO.ElementAt(0));

            var publicationController = new PublicationController(
                                                publicationService.Object,
                                                mapper.Object,
                                                null,
                                                null);

            // Act
            var result = publicationController.Get(_publicationEntities.ElementAt(0).Id);

            // Assert
            Assert.NotNull(publicationController);
            Assert.NotNull(result);
            Assert.Equal("Some text1", result.Value.Text);
        }

        [Fact]
        public void Add_AddPublication_OkObjectResult()
        {
            // Arrange
            var publicationService = new Mock<IPublicationService>();
            var mapper = new Mock<IMapper>();
            var imageSystem = new Mock<IFileSystem<ImageEntity>>();
            var hostEnviroment = new Mock<IHostEnvironment>();

            mapper.Setup(m => m.Map<PublicationDTO>(_publicationEntities.ElementAt(0)))
                .Returns(_publicationsDTO.ElementAt(0));

            var publicationController = new PublicationController(
                                                publicationService.Object,
                                                mapper.Object,
                                                imageSystem.Object,
                                                hostEnviroment.Object);

            // Act
            var result = publicationController.Add(_appPublicationDTO);

            // Assert
            Assert.NotNull(publicationController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Update_UpdatePublication_OkObjectResult()
        {
            // Arrange
            var publicationService = new Mock<IPublicationService>();
            var mapper = new Mock<IMapper>();

            publicationService.Setup(p => p.GetById(_publicationEntities.ElementAt(0).Id))
                .Returns(_publicationEntities.ElementAt(0));

            var publicationController = new PublicationController(
                                                publicationService.Object,
                                                mapper.Object,
                                                null,
                                                null);

            // Act
            var result = publicationController.Update(_updatePublicationDTO);

            // Assert
            Assert.NotNull(publicationController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Delete_DeleteUserById_OkObjectResult()
        {
            // Arrange
            var publicationService = new Mock<IPublicationService>();
            var mapper = new Mock<IMapper>();

            publicationService.Setup(p => p.Delete(_publicationEntities.ElementAt(0).Id))
                .Returns(_publicationEntities.ElementAt(0));

            var publicationController = new PublicationController(
                                                publicationService.Object,
                                                mapper.Object,
                                                null,
                                                null);

            // Act
            var result = publicationController.Delete(_publicationEntities.ElementAt(0).Id);

            // Assert
            Assert.NotNull(publicationController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
