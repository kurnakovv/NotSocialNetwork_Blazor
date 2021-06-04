using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Publication.Add;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Publication.Add.Unsuccesses
{
    public class PublicationControllerUnsuccessTest
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

        private static PublicationEntity _publication = new PublicationEntity()
        {

        };

        [Fact]
        public async Task Add_AddPublicationIfPublicationAlreadyExist_BadRequest400()
        {
            // Arrange
            var addablePublication = new Mock<IAddablePublicationAsync>();
            var mapper = new Mock<IMapper>();

            var publicationController = new PublicationController(
                                                addablePublication.Object,
                                                mapper.Object);

            mapper.Setup(m => m.Map<PublicationEntity>(_addPublicationDTO))
                      .Returns(_publication);

            addablePublication.Setup(ap => ap.AddAsync(_publication))
                                  .Throws(new ObjectAlreadyExistException("Publication already exist."));

            // Act
            var result = await publicationController.Add(_addPublicationDTO);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Add_AddPublicationIfAuthorNotFound_NotFound404()
        {
            // Arrange
            var addablePublication = new Mock<IAddablePublicationAsync>();
            var mapper = new Mock<IMapper>();

            var publicationController = new PublicationController(
                                                addablePublication.Object,
                                                mapper.Object);

            mapper.Setup(m => m.Map<PublicationEntity>(_addPublicationDTO))
                      .Returns(_publication);

            addablePublication.Setup(ap => ap.AddAsync(_publication))
                                  .Throws(new ObjectNotFoundException("Author not found."));

            // Act
            var result = await publicationController.Add(_addPublicationDTO);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
