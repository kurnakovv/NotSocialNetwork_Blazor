using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.UseCases.Publication;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.Publication.Successes
{
    public class EditPublicationSuccessTest
    {
        private readonly PublicationEntity _publication = new PublicationEntity()
        {
            Images = null,
            Author = new UserEntity() { Name = "Name", DateOfBirth = DateTime.Now, Email = "some@gmail.com" },
        };

        [Fact]
        public async Task UpdateAsync_UpdatePublication_Publication()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var publicationRepository = new Mock<IRepositoryAsync<PublicationEntity>>();
            var editPublication = new EditPublication(
                                            getablePublication.Object,
                                            publicationRepository.Object);

            getablePublication.Setup(gp => gp.GetById(_publication.Id))
                                  .Returns(_publication);


            // Act
            var result = await editPublication.UpdateAsync(_publication);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publication, result);
            Assert.Equal(_publication.Id, result.Id);
        }

        [Fact]
        public async Task DeleteAsync_DeletePublication_Publication()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var publicationRepository = new Mock<IRepositoryAsync<PublicationEntity>>();
            var editPublication = new EditPublication(
                                            getablePublication.Object,
                                            publicationRepository.Object);

            getablePublication.Setup(gp => gp.GetById(_publication.Id))
                                  .Returns(_publication);

            // Act
            var result = await editPublication.DeleteAsync(_publication.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publication, result);
            Assert.Equal(_publication.Id, result.Id);
        }
    }
}
