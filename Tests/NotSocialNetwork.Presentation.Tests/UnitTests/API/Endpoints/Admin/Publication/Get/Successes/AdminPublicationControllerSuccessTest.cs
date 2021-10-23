using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Admin.Publication.Get;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Admin.Publication.Get.Successes
{
    public class AdminPublicationControllerSuccessTest
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
                Author = new UserDTO()
                {
                    Id = _author.Id,
                    Email = _author.Email,
                    Image = _author.Image,
                    Name = _author.Name
                },
                Text = "Some text1",
            },
            new PublicationDTO()
            {
                Author = new UserDTO()
                {
                    Id = _author.Id,
                    Email = _author.Email,
                    Image = _author.Image,
                    Name = _author.Name
                },
                Text = "Some text2",
            },
        };

        private static IEnumerable<PublicationEntity> _publicationEntities = new List<PublicationEntity>()
        {
            new PublicationEntity()
            {
                Author = _author,
                Text = _publicationsDTO.ElementAt(0).Text,
            },
            new PublicationEntity()
            {
                Author = _author,
                Text = _publicationsDTO.ElementAt(1).Text,
            },
        };

        [Fact]
        public void Get_GetAllPublications_OkObjectResult()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var mapper = new Mock<IMapper>();

            getablePublication.Setup(gp => gp.GetAll())
                .Returns(_publicationEntities);

            mapper.Setup(p => p.Map<IEnumerable<PublicationDTO>>(_publicationEntities))
                .Returns(_publicationsDTO);

            var adminPublicationController = new AdminPublicationController(
                                            getablePublication.Object,
                                            mapper.Object);

            // Act
            var results = adminPublicationController.Get();

            // Assert
            Assert.NotNull(adminPublicationController);
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results.Result);
        }

        [Fact]
        public void Get_GetPublicationById_OkObjectResult()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var mapper = new Mock<IMapper>();

            getablePublication.Setup(gp => gp.GetById(_publicationEntities.ElementAt(0).Id))
                .Returns(_publicationEntities.ElementAt(0));

            mapper.Setup(m => m.Map<PublicationDTO>(_publicationEntities.ElementAt(0)))
                .Returns(_publicationsDTO.ElementAt(0));

            var adminPublicationController = new AdminPublicationController(
                                                getablePublication.Object,
                                                mapper.Object);

            // Act
            var result = adminPublicationController.Get(_publicationEntities.ElementAt(0).Id);

            // Assert
            Assert.NotNull(adminPublicationController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllByAuthorId_GetAllByAuthorId_Publications_OkObjectResult()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var mapper = new Mock<IMapper>();

            getablePublication.Setup(gp => gp.GetAllByAuthorId(_publicationEntities.ElementAt(0).Author.Id))
                .Returns(_publicationEntities);

            mapper.Setup(m => m.Map<PublicationDTO>(_publicationEntities.ElementAt(0)))
                .Returns(_publicationsDTO.ElementAt(0));

            var adminPublicationController = new AdminPublicationController(
                                                getablePublication.Object,
                                                mapper.Object);

            // Act
            var result = adminPublicationController.GetAllByAuthor(_publicationEntities.ElementAt(0).Author.Id);

            // Assert
            Assert.NotNull(adminPublicationController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
