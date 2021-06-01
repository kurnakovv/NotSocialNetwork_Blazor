using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Publication.Add;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using System;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Publication.Add.Successes
{
    public class PublicationControllerSuccessTest
    {
        private static UserEntity _author = new UserEntity()
        {
            Name = "Ivan",
            DateOfBirth = DateTime.Now,
            Email = "ivan@gmail.com"
        };

        private static AddPublicationDTO _addPublicationDTO = new AddPublicationDTO()
        {
            Text = "Some text",
            AuthorId = _author.Id,
        };

        [Fact]
        public void Add_AddPublication_OkObjectResult()
        {
            // Arrange
            var addablePublication = new Mock<IAddablePublication>();
            var mapper = new Mock<IMapper>();

            var publicationController = new PublicationController(
                                                addablePublication.Object,
                                                mapper.Object);

            // Act
            var result = publicationController.Add(_addPublicationDTO);

            // Assert
            Assert.NotNull(publicationController);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
