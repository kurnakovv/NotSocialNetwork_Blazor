using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.UseCases.Publication;
using System;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.Publication.Unsuccesses
{
    public class EditPublicationUnsuccessTest
    {
        private readonly PublicationEntity _publication = new PublicationEntity()
        {
            Images = null,
            Author = new UserEntity() { Name = "Name", DateOfBirth = DateTime.Now, Email = "some@gmail.com" },
        };

        [Fact]
        public void Update_UpdateInvalidPublication_ObjectNotFoundException()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var publicationRepository = new Mock<IRepository<PublicationEntity>>();
            var editPublication = new EditPublication(
                                            getablePublication.Object,
                                            publicationRepository.Object);

            getablePublication.Setup(gp => gp.GetById(_publication.Id))
                                .Throws(new ObjectNotFoundException($"Publication by Id: {_publication.Id} not found."));

            // Act
            Action act = () => editPublication.Update(_publication);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void Delete_DeleteInvalidPublication_ObjectNotFoundException()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var publicationRepository = new Mock<IRepository<PublicationEntity>>();
            var editPublication = new EditPublication(
                                            getablePublication.Object,
                                            publicationRepository.Object);

            getablePublication.Setup(gp => gp.GetById(_publication.Id))
                                  .Throws(new ObjectNotFoundException($"Publication by Id: {_publication.Id} not found."));

            // Act
            Action act = () => editPublication.Delete(_publication.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }
    }
}
