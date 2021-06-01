using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Publication.Edit;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Publication.Edit.Successes
{
    public class PublicationControllerSuccessTest
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

        private static UpdatePublicationDTO _updatePublicationDTO = new UpdatePublicationDTO()
        {
            Id = _publicationEntities.ElementAt(0).Id,
            Text = "Update text",
        };

        [Fact]
        public void Update_UpdatePublication_OkObjectResult()
        {
            // Arrange
            var editablePublication = new Mock<IEditablePublication>();
            var getablePublication = new Mock<IGetablePublication>();
            var mapper = new Mock<IMapper>();

            getablePublication.Setup(ep => ep.GetById(_publicationEntities.ElementAt(0).Id))
                .Returns(_publicationEntities.ElementAt(0));

            var publicationController = new PublicationController(
                                                editablePublication.Object,
                                                getablePublication.Object,
                                                mapper.Object);

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
            var editablePublication = new Mock<IEditablePublication>();
            var getablePublication = new Mock<IGetablePublication>();
            var mapper = new Mock<IMapper>();

            getablePublication.Setup(ep => ep.GetById(_publicationEntities.ElementAt(0).Id))
                .Returns(_publicationEntities.ElementAt(0));

            var publicationController = new PublicationController(
                                                editablePublication.Object,
                                                getablePublication.Object,
                                                mapper.Object);

            // Act
            var result = publicationController.Delete(_publicationEntities.ElementAt(0).Id);

            // Assert
            Assert.NotNull(publicationController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
