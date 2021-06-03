using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.UseCases.Publication;
using System;
using System.Threading.Tasks;
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
        public async Task UpdateAsync_UpdateInvalidPublication_ObjectNotFoundException()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var publicationRepository = new Mock<IRepositoryAsync<PublicationEntity>>();
            var editPublication = new EditPublication(
                                            getablePublication.Object,
                                            publicationRepository.Object);

            getablePublication.Setup(gp => gp.GetById(_publication.Id))
                                .Throws(new ObjectNotFoundException($"Publication by Id: {_publication.Id} not found."));

            // Act
            Func<Task> act = async () => await editPublication.UpdateAsync(_publication);

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(act);
        }

        [Fact]
        public async Task DeleteAsync_DeleteInvalidPublication_ObjectNotFoundException()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var publicationRepository = new Mock<IRepositoryAsync<PublicationEntity>>();
            var editPublication = new EditPublication(
                                            getablePublication.Object,
                                            publicationRepository.Object);

            getablePublication.Setup(gp => gp.GetById(_publication.Id))
                                  .Throws(new ObjectNotFoundException($"Publication by Id: {_publication.Id} not found."));

            // Act
            Func<Task> act = async () => await editPublication.DeleteAsync(_publication.Id);

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(act);
        }
    }
}
